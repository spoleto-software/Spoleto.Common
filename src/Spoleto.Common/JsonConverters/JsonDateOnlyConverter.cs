namespace Spoleto.Common.JsonConverters
{
    public class JsonDateOnlyConverter : JsonDateTimeConverterBase
    {
        protected JsonDateOnlyConverter() : base("yyyy-MM-dd")
        {
        }
    }
}
