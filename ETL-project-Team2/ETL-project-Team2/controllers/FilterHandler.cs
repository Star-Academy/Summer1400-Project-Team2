using ETL_project_Team2.services;

namespace ETL_project_Team2.controllers
{
    public class FilterHandler : IFilterHandler
    {
        private IFilterService _filterService;
        public FilterHandler(IFilterService filterService)
        {
            this._filterService = filterService;
        }
        
        public void Operate(string query)
        {
            throw new System.NotImplementedException();
        }
    }
}