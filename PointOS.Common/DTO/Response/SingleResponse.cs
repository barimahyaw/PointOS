namespace PointOS.Common.DTO.Response
{
    public class SingleResponse<T> where T : class
    {
        public ResponseHeader ResponseHeader { get; set; }
        public T ResponseBody { get; set; }

        public SingleResponse() { }

        public SingleResponse(ResponseHeader responseHeader, T responseBody)
        {
            ResponseHeader = responseHeader;
            ResponseBody = responseBody;
        }
    }
}
