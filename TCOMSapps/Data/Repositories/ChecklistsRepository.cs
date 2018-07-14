using TCOMSapps.Data.Repositories.Interfaces;

namespace TCOMSapps.Data.Repositories
{
  public class ChecklistsRepository : IChecklistsRepository
  {


    public ChecklistsRepository(IApplicationDbContext context)
    {
      Context = context;
    }

    public IApplicationDbContext Context { get; private set; }




    public void Dispose()
    {
      Context.Dispose();
    }
  }
}
