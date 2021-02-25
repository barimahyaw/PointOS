using System.Collections.Generic;

namespace PointOS.Common.DTO.Response
{
    public class ListResponse<T> where T : class
    {
        public ResponseHeader ResponseHeader { get; set; }
        public IEnumerable<T> ResponseBodyList { get; set; }

        public ListResponse(ResponseHeader responseHeader, IEnumerable<T> responseBodyList)
        {
            ResponseHeader = responseHeader;
            ResponseBodyList = responseBodyList;
        }

        public ListResponse() { }
    }
}
