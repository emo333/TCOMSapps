using System;

namespace TCOMSapps.Data.Repositories.Interfaces
{
  public interface IChecklistsRepository : IDisposable
  {


    IApplicationDbContext Context { get; }

  }


}
