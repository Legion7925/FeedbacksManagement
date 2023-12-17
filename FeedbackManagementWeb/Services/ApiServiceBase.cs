using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManagementWeb.Services;

public class ApiServiceBase
{
    protected IHttpClientFactory _httpClientFactory;

    public ApiServiceBase(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
}
