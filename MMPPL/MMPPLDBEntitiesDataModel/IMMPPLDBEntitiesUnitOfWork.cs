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
    /// IMMPPLDBEntitiesUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface IMMPPLDBEntitiesUnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// The ColorMaster entities repository.
        /// </summary>
        IRepository<ColorMaster, decimal> ColorMasters { get; }

        /// <summary>
        /// The CustmerMaster entities repository.
        /// </summary>
        IRepository<CustmerMaster, decimal> CustmerMasters { get; }

        /// <summary>
        /// The Dispatch entities repository.
        /// </summary>
        IRepository<Dispatch, decimal> Dispatches { get; }

        /// <summary>
        /// The DuplicatePrinting entities repository.
        /// </summary>
        IReadOnlyRepository<DuplicatePrinting> DuplicatePrintings { get; }

        /// <summary>
        /// The GradeMaster entities repository.
        /// </summary>
        IRepository<GradeMaster, decimal> GradeMasters { get; }

        /// <summary>
        /// The LabelPrinting entities repository.
        /// </summary>
        IRepository<LabelPrinting, decimal> LabelPrintings { get; }

        /// <summary>
        /// The MicronMaster entities repository.
        /// </summary>
        IRepository<MicronMaster, decimal> MicronMasters { get; }

        /// <summary>
        /// The SizeMaster entities repository.
        /// </summary>
        IRepository<SizeMaster, decimal> SizeMasters { get; }

        /// <summary>
        /// The StockReport entities repository.
        /// </summary>
        IReadOnlyRepository<StockReport> StockReports { get; }
    }
}
