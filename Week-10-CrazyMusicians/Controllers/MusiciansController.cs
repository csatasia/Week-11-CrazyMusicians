using Microsoft.AspNetCore.Mvc;
using Week_11_CrazyMusicians.Models;

namespace Week_11_CrazyMusicians.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ResponseCache(Duration = 60, NoStore =true)] // http isteklerini 60 saniye cachler tarayıcıda. No store özelliği true olduğunda cachleme çalışmaz.
    public class MusiciansController : ControllerBase
    {
        private static List<CrazyMusician> crazyMusicians = new()
        {
            new() { Name = "Ahmet Çalgı", Job = "Ünlü Çalgı Çalar", Description = "Her zaman yanlış nota çalar ama çok eğlencelidir."},
            new() { Name = "Zeynep Melodi", Job = "Popüler Melodi Yazarı", Description = "Şarkıları yanlış anlaşılır ama çok popülerdir."},
            new() { Name = "Cemil Akor", Job = "Çılgın Akorist", Description = "Akorları sık değişir ama şaşırtıcı derecede yeteneklidir."},
            new() { Name = "Fatma Nota", Job = "Süpriz Nota Üreticisi", Description = "Nota üretirken sürekli süprizler hazırlar."},
            new() { Name = "Hasan Ritim", Job = "Ritim Canavarı", Description = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir."},
            new() { Name = "Elif Armoni", Job = "Armoni Ustası", Description = "Armonilerini bazen çok yanlış çalar ama çok yaratıcıdır."},
            new() { Name = "Ali Perde", Job = "Perde Uygulayıcı", Description = "Her perdeyi farklı şekilde çalar, her zaman süprizlidir."},
            new() { Name = "Ayşe Rezonans", Job = "Rezonans Uzmanı", Description = "Rezonans konusunda uzman ama bazen çok gürültü çıkartır."},
            new() { Name = "Murat Ton", Job = "Tonlama Meraklısı", Description = "Tonlamalarında ki farklılılar bazen komik ama oldukça ilginç."},
            new() { Name = "Selin Akor", Job = "Akor Sihirbazı", Description = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır."},
        };


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(crazyMusicians.OrderBy(x=>x.Name));
        }

        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return Ok(crazyMusicians.OrderBy(x => x.Name));

            var result = crazyMusicians
                .Where(x=>x.Name
                    .Contains(name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x=>x.Name);

            if (!result.Any())
            {
                return NotFound($"No musicians found containing '{name}' in their name.");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) 
        {
            var musician = crazyMusicians.Find(x => x.Id == id);

            if (musician is null)
                return NotFound($"Musician with Id {id} not found.");

            return Ok(musician);
        }

        [HttpPost]
        public IActionResult Add([FromBody]CrazyMusician crazyMusician)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = errors
                });
            }

            CrazyMusician newCrazyMusician = new()
            {
                Name = crazyMusician.Name,
                Job = crazyMusician.Job,
                Description = crazyMusician.Description
            };

            crazyMusicians.Add(newCrazyMusician);
            return CreatedAtAction(nameof(Add), $"Musician with Id {crazyMusician.Id} has been added.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] CrazyMusician updatedMusician)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = errors
                });
            }

            var musician = crazyMusicians.Find(x => x.Id == id);

            if (musician is null)
                return NotFound($"Musician with Id {id} not found.");

            musician.Name = updatedMusician.Name;
            musician.Job = updatedMusician.Job;
            musician.Description = updatedMusician.Description;

            return Ok($"Musician with Id {id} has been updated.");
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(Guid id, [FromBody] CrazyMusician patchUpdatedMusician)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = errors
                });
            }

            var musician = crazyMusicians.Find(x => x.Id == id);

            if (musician is null)
                return NotFound($"Musician with Id {id} not found.");

            if (patchUpdatedMusician.Name is not null)
                musician.Name = patchUpdatedMusician.Name;

            if (patchUpdatedMusician.Job is not null)
                musician.Job = patchUpdatedMusician.Job;

            if (patchUpdatedMusician.Description is not null)
                musician.Description = patchUpdatedMusician.Description;

            return Ok($"Musician with Id {id} has been updated.");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var musician = crazyMusicians.Find(x => x.Id == id);

            if (musician is null)
                return NotFound($"Musician with Id {id} not found.");

            crazyMusicians.Remove(musician);
            return Ok($"Musician with Id {id} has been deleted.");
        }

    }
}
