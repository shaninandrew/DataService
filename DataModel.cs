namespace MyService
{
    public class Personal
    {
        /// <summary>
        /// Id � ���� ������
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// ��� ������������
        /// </summary>
        public string SNA { get; set; }

        /// <summary>
        /// ����� ������
        /// </summary>
        public string Departament { get; set; }

        /// <summary>
        /// ���� ��������
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// ���� ������ ������ � ���������
        /// </summary>
        public DateTime StartToWorkDate { get; set;}

        /// <summary>
        /// ���� ����������
        /// </summary>
        public DateTime EndToWorkDate { get;set; }

        /// <summary>
        /// ������ ���������� �����
        /// </summary>
        public float Salary { get; set; } = 10000;

    }
}