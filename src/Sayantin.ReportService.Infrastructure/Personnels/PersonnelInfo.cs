using ApiCallManager;
using ReportService.Domain.Exceptions;
using ReportService.Domain.Personnels;
using ReportService.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace ReportService.Infrastructure.Personnels
{
    public class PersonnelInfo(IApiManager _ApiManager, IOptions<ApiEndpointsOptions> options) : IPersonnelInfo
    {
        public async Task<List<Personnel>> GetPersonnels(PersonnelSearchInput input)
        {
            var Result = await _ApiManager.PostAsync<PersonnelSearchInput, List<Personnel>>(options.Value.GatewayAddress + "/Pb/Pb/GetPersonnelName", input);

            if (!Result.IsSuccess)
                throw new BaseException($"GetPersonnelNames exception. Exception={Result?.Problem?.Title ?? string.Empty} - {Result?.Problem?.Detail ?? string.Empty}");


            return Result.Result;
        }
    }
}
