using System.Text.Json;
using API.Helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response,
        int currentPage, int itemsPerPage, int totalItems, int totalPages
        ){
            var paginationHeader = new PaginationHeader(currentPage,itemsPerPage,totalItems, totalPages);

            var opt = new JsonSerializerOptions(){
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination",JsonSerializer.Serialize(paginationHeader,opt));
            // Access-Control-Expose-Headers (optional) - The XMLHttpRequest 2 object has a getResponseHeader() method that returns the value of a particular response header. During a CORS request, the getResponseHeader() method can only access simple response headers. Simple response headers are defined as follows:
            // Cache-Control
            // Content-Language
            // Content-Type
            // Expires
            // Last-Modified
            // Pragma
            // If you want clients to be able to access other headers, you have to use the Access-Control-Expose-Headers header. The value of this header is a comma-delimited list of response headers you want to expose to the client.
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
}