using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using MMPPL.Common.Utils;
using MMPPL.MMPPLDBEntitiesDataModel;
using MMPPL.Common.DataModel;
using MMPPL;
using MMPPL.Common.ViewModel;

namespace MMPPL.ViewModels
{
    /// <summary>
    /// Represents the single CustmerMaster object view model.
    /// </summary>
    public partial class CustmerMasterViewModel : SingleObjectViewModel<CustmerMaster, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of CustmerMasterViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CustmerMasterViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new CustmerMasterViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CustmerMasterViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CustmerMasterViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CustmerMasterViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.CustmerMasters, x => x.CustomerName)
        {
        }

        /// <summary>
        /// The view model for the CustmerMasterDispatches detail collection.
        /// </summary>
        public CollectionViewModel<Dispatch, decimal, IMMPPLDBEntitiesUnitOfWork> CustmerMasterDispatchesDetails
        {
            get { return GetDetailsCollectionViewModel((CustmerMasterViewModel x) => x.CustmerMasterDispatchesDetails, x => x.Dispatches, x => x.CustomerId, (x, key) => { x.CustomerId = key; }); }
        }
    }
}
