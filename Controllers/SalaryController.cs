using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Data.Entity;

namespace MyService.Controllers
{
    [ApiController]

    ///<summary>
    /// ���� ������� ������
    /// </summary>
    [Route("/api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        DataBase db = new DataBase();

        /// <summary>
        /// ������ �������
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
        /// ���������� ������ �����������
        /// 
        /// </summary>
        /// <param name="id">id ������</param>
        /// <param name="limit"> ������ </param>
        /// <param name="sort"> ���� ���������� </param>
        /// <param name="desc"> �������� ���������� </param>
        /// <returns>������ �������� � ��</returns>
        [HttpGet(Name = "GetPersonal")]
        public async Task<IEnumerable<Personal>> Get(string? id, int? limit, string? sort, bool? desc)
        {

            if (sort == null)
            {
                sort = "SNA";
            }


            if (id != null)
            {
                return await Task.FromResult( db.Personals.Where(p => p.Id.Equals(id)));
            }
            else
            {

                switch (sort.ToLower())
                {
                    case "salary":
                        if (limit.HasValue)
                            return await (!desc.HasValue || desc.Value==false   ? Task.FromResult(db.Personals.OrderBy(p => p.Salary).Take(limit.Value).ToArray()) :  Task.FromResult(db.Personals.OrderByDescending(p => p.Salary).Take(limit.Value).ToArray()) );
                        else
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.Salary).ToArray()) : Task.FromResult(db.Personals.OrderByDescending(p => p.Salary).ToArray()) );
                        break;

                    case "sna":
                        if (limit.HasValue)
                            //return await Task.FromResult(db.Personals.OrderBy(p => p.Salary).Take<Personal>(limit.Value));
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.SNA).Take(limit.Value).ToArray()) :  Task.FromResult(db.Personals.OrderByDescending(p => p.SNA).Take(limit.Value).ToArray()) );
                        else
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.SNA).ToArray()) : Task.FromResult(db.Personals.OrderByDescending(p => p.SNA).ToArray()) ); 
                        break;

                    case "starttoworkdate":
                        if (limit.HasValue)
                            //return await Task.FromResult(db.Personals.OrderBy(p => p.Salary).Take<Personal>(limit.Value));
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.StartToWorkDate).Take(limit.Value).ToArray()) : Task.FromResult(db.Personals.OrderByDescending(p => p.StartToWorkDate).Take(limit.Value).ToArray())  );
                        else
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.StartToWorkDate).ToArray()) :Task.FromResult(db.Personals.OrderByDescending(p => p.StartToWorkDate).ToArray())  ); 
                        break;

                    case "departament":
                        if (limit.HasValue)
                            //return await Task.FromResult(db.Personals.OrderBy(p => p.Salary).Take<Personal>(limit.Value));
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.Departament).Take(limit.Value).ToArray()) :  Task.FromResult(db.Personals.OrderByDescending(p => p.Departament).Take(limit.Value).ToArray())  );
                        else
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.Departament).ToArray()) :  Task.FromResult(db.Personals.OrderByDescending(p => p.Departament).ToArray()) );
                        break;

                    case "birthdate":
                        if (limit.HasValue)
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.BirthDate).Take(limit.Value).ToArray()) : Task.FromResult(db.Personals.OrderByDescending(p => p.BirthDate).Take(limit.Value).ToArray()) );
                        else
                            return await (!desc.HasValue || desc.Value == false  ? Task.FromResult(db.Personals.OrderBy(p => p.BirthDate).ToArray()) :  Task.FromResult(db.Personals.OrderByDescending(p => p.BirthDate).ToArray())  );
                        break;

                    case "endtoworkdate":
                        if (limit.HasValue)
                            //return await Task.FromResult(db.Personals.OrderBy(p => p.Salary).Take<Personal>(limit.Value));
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.EndToWorkDate).Take(limit.Value).ToArray()) :  Task.FromResult(db.Personals.OrderByDescending(p => p.EndToWorkDate).Take(limit.Value).ToArray())  );
                        else
                            return await (!desc.HasValue || desc.Value == false ? Task.FromResult(db.Personals.OrderBy(p => p.EndToWorkDate).ToArray()) :  Task.FromResult(db.Personals.OrderByDescending(p => p.BirthDate).ToArray())  );
                        break;

                }

                //�� ������� ��� ����������
                return await Task.FromResult(db.Personals.ToArray());

            }

        }


        /// <summary>
        /// ���������� ������
        /// </summary>
        /// <param name="p">������������ ������</param>
        [HttpPut(Name ="InsertPersonal" )]
        public async Task<string> Post([FromBody]  Personal p)
        {
            //�������� ������
            p.Id=Guid.NewGuid().ToString();
            db.Personals.Add(p);
            await db.SaveChangesAsync();
            return (p.Id);

        }

        /// <summary>
        /// ������� ������ �� ���� ������ �� Id
        /// </summary>
        /// <param name="id">ID ��������� ������</param>        
        [HttpDelete(Name = "DeletePersonal")]
        public void Delete(string id) 
        {
            foreach(Personal p in db.Personals.Where (p=>p.Id.Equals(id))) 
                db.Personals.Remove( p);
            db.SaveChanges();
        }

        /// <summary>
        /// ���������� ��������
        /// </summary>
        /// <param name="field">����</param>
        /// <param name="value">��������</param>
        [HttpPatch(Name = "UpdatePersonal")]
        public async void PatchUpdate(string id, string field, string value)
        {
            //�������� ������

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