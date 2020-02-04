namespace WebApiCommon.DataModel
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Code { get; set; }

        public Hive Hive { get; set; }
    }
}
