using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using MMPPL.Common.Utils;
using MMPPL.Common.DataModel;
using MMPPL.Common.DataModel.EntityFramework;
using MMPPL;

namespace MMPPL.MMPPLDBEntitiesDataModel
{
    /// <summary>
    /// A MMPPLDBEntitiesUnitOfWork instance that represents the run-time implementation of the IMMPPLDBEntitiesUnitOfWork interface.
    /// </summary>
    public class MMPPLDBEntitiesUnitOfWork : DbUnitOfWork<MMPPLDBEntities>, IMMPPLDBEntitiesUnitOfWork
    {

        public MMPPLDBEntitiesUnitOfWork(Func<MMPPLDBEntities> contextFactory)
            : base(contextFactory)
        {
        }

        IRepository<ColorMaster, decimal> IMMPPLDBEntitiesUnitOfWork.ColorMasters
        {
            get { return GetRepository(x => x.Set<ColorMaster>(), (ColorMaster x) => x.ColorId); }
        }

        IRepository<CustmerMaster, decimal> IMMPPLDBEntitiesUnitOfWork.CustmerMasters
        {
            get { return GetRepository(x => x.Set<CustmerMaster>(), (CustmerMaster x) => x.CustId); }
        }

        IRepository<Dispatch, decimal> IMMPPLDBEntitiesUnitOfWork.Dispatches
        {
            get { return GetRepository(x => x.Set<Dispatch>(), (Dispatch x) => x.DptId); }
        }

        IReadOnlyRepository<DuplicatePrinting> IMMPPLDBEntitiesUnitOfWork.DuplicatePrintings
        {
            get { return GetReadOnlyRepository(x => x.Set<DuplicatePrinting>()); }
        }

        IRepository<GradeMaster, decimal> IMMPPLDBEntitiesUnitOfWork.GradeMasters
        {
            get { return GetRepository(x => x.Set<GradeMaster>(), (GradeMaster x) => x.GradeId); }
        }

        IRepository<LabelPrinting, decimal> IMMPPLDBEntitiesUnitOfWork.LabelPrintings
        {
            get { return GetRepository(x => x.Set<LabelPrinting>(), (LabelPrinting x) => x.LblId); }
        }

        IRepository<MicronMaster, decimal> IMMPPLDBEntitiesUnitOfWork.MicronMasters
        {
            get { return GetRepository(x => x.Set<MicronMaster>(), (MicronMaster x) => x.MicronId); }
        }

        IRepository<SizeMaster, decimal> IMMPPLDBEntitiesUnitOfWork.SizeMasters
        {
            get { return GetRepository(x => x.Set<SizeMaster>(), (SizeMaster x) => x.SizeId); }
        }

        IReadOnlyRepository<StockReport> IMMPPLDBEntitiesUnitOfWork.StockReports
        {
            get { return GetReadOnlyRepository(x => x.Set<StockReport>()); }
        }
    }
}
