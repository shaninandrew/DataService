using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Data.Entity;

namespace MyService.Controllers
{
    [ApiController]

    ///<summary>
    /// Путь запроса данных
    /// </summary>
    [Route("/api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        DataBase db = new DataBase();

        /// <summary>
        /// Журнал событий
        /// </summary>
        /// <param name="logger"></param>
        public SalaryController(ILogger<SalaryController> logger)
        {
            _logger = logger;
            Console.WriteLine("* Start...");

           // db.ConfigureAwait(false);
           // db.Database.EnsureCreated();
            

        }

        /// <summary>
        /// Возвращает список сотрудников
        /// 
        /// </summary>
        /// <param name="id">id записи</param>
        /// <param name="limit"> размер </param>
        /// <param name="sort"> поле </param>
        /// <returns></returns>
        [HttpGet(Name = "GetPersonal")]
        public async Task<IEnumerable<Personal>> Get(string? id, int? limit, string? sort)
        {

            if (sort == null)
            {
                sort = "SNA";
            }


            if (id != null)
            {
                return await db.Personals.Where(p => p.Id.Equals(id)).ToArrayAsync();
            }
            else
            {

                switch (sort.ToLower())
                {
                    case "salary":
                        if (limit.HasValue)
                            return await Task.FromResult(db.Personals.OrderBy(p => p.Salary).Take<Personal>(limit.Value));
                        else
                            return await Task.FromResult(db.Personals.OrderBy(p => p.Salary).ToArray());
                        break;

                    case "sna":
                        if (limit.HasValue)
                            return await Task.FromResult(db.Personals.OrderBy(p => p.SNA).Take<Personal>(limit.Value));
                        else
                            return await Task.FromResult(db.Personals.OrderBy(p => p.SNA).ToArray());
                        break;

                    case "starttoworkdate":
                        if (limit.HasValue)
                            return await Task.FromResult(db.Personals.OrderBy(p => p.StartToWorkDate).Take<Personal>(limit.Value));
                         else
                            return await Task.FromResult(db.Personals.OrderBy(p => p.StartToWorkDate).ToArray());
                        break;

                    case "departament":
                        if (limit.HasValue)
                            return await Task.FromResult(db.Personals.OrderBy(p => p.Departament).Take<Personal>(limit.Value));
                        else
                            return await Task.FromResult(db.Personals.OrderBy(p => p.Departament).ToArray());
                        break;

                    case "birthdate":
                        if (limit.HasValue)
                            return await Task.FromResult(db.Personals.OrderBy(p => p.BirthDate).Take<Personal>(limit.Value));
                        else
                            return await Task.FromResult(db.Personals.OrderBy(p => p.BirthDate).ToArray());
                        break;

                    case "endtoworkdate":
                        if (limit.HasValue)
                            return await Task.FromResult(db.Personals.OrderBy(p => p.EndToWorkDate).Take<Personal>(limit.Value));
                        else
                            return await Task.FromResult(db.Personals.OrderBy(p => p.EndToWorkDate).ToArray());
                        break;

                }

                //По дефолту без сортировку
                return await Task.FromResult(db.Personals.ToArray());

            }
            
            
            /*return Enumerable.Range(1, 5).Select(index => new Personal
            {
                Id = Guid.NewGuid().ToString(),
                Salary = 10000,
                BirthDate = DateTime.Parse("12.01.1966"),
                StartToWorkDate = DateTime.Now,
                EndToWorkDate = DateTime.Now.AddDays(365),
                Departament = "Departa #1"

            }).ToArray();*/
        }


        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="p">Персональные данные</param>
        [HttpPut(Name ="InsertPersonal" )]
        public async Task<string> Post([FromBody]  Personal p)
        {
            //Обновить данных
            p.Id=Guid.NewGuid().ToString();
            db.Personals.Add(p);
            await db.SaveChangesAsync();
            return (p.Id);

        }

        
        [HttpDelete(Name = "DeletePersonal")]
        /// <summary>
        /// Удаляет запись из базы данных по Id
        /// </summary>
        /// <param name="id">ID удаляемой записи</param>
        public void Delete(string id) 
        {
            foreach(Personal p in db.Personals.Where (p=>p.Id.Equals(id))) 
                db.Personals.Remove( p);
            db.SaveChanges();
        }

        /// <summary>
        /// Обновление значений
        /// </summary>
        /// <param name="field">Поле</param>
        /// <param name="value">Значение</param>
        [HttpPatch(Name = "UpdatePersonal")]
        public async void PatchUpdate(string id, string field, string value)
        {
            //Обновить данных

            Personal x=  db.Personals.Where(p => p.Id.Equals(id)).Single<Personal>();
           
            switch (field.ToLower())
            {
               case "salary": 
                    x.Salary = float.Parse(value);
                    break;

                case "sna":
                    x.SNA = value;
                    break;

                case "birthdate":
                    x.BirthDate = DateTime.Parse(value);
                    break;

                case "endtoworkdate":
                    x.EndToWorkDate = DateTime.Parse(value);
                    break;

                case "departament":
                    x.Departament = value;
                    break;

                    

            }
            db.SaveChangesAsync();
        }


    }
}