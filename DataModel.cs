namespace MyService
{
    public class Personal
    {
        /// <summary>
        /// Id в базе данных
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string SNA { get; set; }

        /// <summary>
        /// Отдел работы
        /// </summary>
        public string Departament { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Дата начала работы в должности
        /// </summary>
        public DateTime StartToWorkDate { get; set;}

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime EndToWorkDate { get;set; }

        /// <summary>
        /// Размер заработной платы
        /// </summary>
        public float Salary { get; set; } = 10000;

    }
}