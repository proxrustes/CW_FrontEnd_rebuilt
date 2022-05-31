using System.Collections.Generic;

namespace CW_FrontEnd_rebuilt.ApiManager
{
    public interface IApiController<Model> where Model : class
    {
        Model Get(int id);
        List<Model> GetAll();
        void Add(Model model);
        void Update(Model model);
        void Delete(int id);
    }
}
