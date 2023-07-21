using WebServiceDomain.Models;

namespace WebServiceDomain.Services
{
    public interface IDataService
    {
        Task<bool> Create(string key, string value);

        #region Потенциальные контракты
        //Task<(int pagesCount, List<Jsontable>)> GetAllAsync(int pageIndex = 1, int pageSize = 20);

        //Task<bool> Update(int id, Jsontable entity);

        //Task<bool> Delete(Jsontable entity);
        #endregion
    }
}
