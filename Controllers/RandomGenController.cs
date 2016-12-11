using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PerftestCore.Models;

namespace PerftestCore.Controllers
{
    class Result
    {
        public int sample_size { get; set; }
        public Double[] data { get; set; }
    }

    public class RandomGenController : Controller
    {
        private readonly Random _rand;
        private readonly PerfTestSettings _settings;
        public RandomGenController(IOptions<PerfTestSettings> settingsOptions)
        {
            _rand = new Random();
            _settings = settingsOptions.Value;
        }

        private Double[] generateRandomNumbers(int sample_size)
        {
            var l = new Double[sample_size];

            for (var i = 0; i < sample_size; i++)
            {
                l[i] = _rand.NextDouble();
            }

            return l;
        }

        private Double median(Double[] d)
        {
            Array.Sort(d);
            double mid = (d.Length - 1) / 2.0;
            return (d[(int)mid] + d[(int)(mid + 0.5)] / 2);
        }

        [Route("raw/{sample_size}")]
        public IActionResult GenRandoms(int sample_size)
        {
            var result = new Result { sample_size = sample_size, data = generateRandomNumbers(sample_size) };

            return new ObjectResult(result);
        }

        [Route("local/{sample_size}")]
        public IActionResult PerftestLocal(int sample_size)
        {
            var l = new Double[] { median(generateRandomNumbers(sample_size)) };

            var result = new Result { sample_size = sample_size, data = l };

            return new ObjectResult(result);
        }

        [Route("remote/{sample_size}")]
        public IActionResult PerftestRemote(int sample_size)
        {
            var l = new Double[] { median(generateRandomNumbers(sample_size)) };

            var result = new Result { sample_size = sample_size, data = l };

            return new ObjectResult(result);
        }

    }

}