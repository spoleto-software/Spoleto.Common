namespace Spoleto.Common.JsonConverters
{
    public class JsonDateOnlyConverter : JsonDateTimeConverterBase
    {
        public JsonDateOnlyConverter() : base("yyyy-MM-dd")
        {
        }
    }
}
