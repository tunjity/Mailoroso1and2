namespace msql_mongo_crud.Model
{
    public class ReturnObject
    {
        public string id { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
}
