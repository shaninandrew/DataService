using MyService;


namespace Test_check
{
    [TestClass]
    public class MakeTake
    {
        [TestMethod]
        public void Check_one()
        {
            Personal personal = new Personal();
            Assert.IsNotNull(personal);
            
            personal.Departament = "xxxx";
            personal.SNA = "test";
            personal.BirthDate = DateTime.Now;
            personal.Salary = 10000;

            personal.Id = "000-0000-2233";
            
            //ѕроверка мен€етс€ ли что-то по автомату
            Assert.AreEqual(personal.Salary,10000);
            Assert.AreEqual(personal.Id, "000-0000-2233");
            Assert.AreEqual(personal.Departament, "xxxx");
            

        }
    }
}