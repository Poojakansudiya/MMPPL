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
    /// Represents the single Dispatch object view model.
    /// </summary>
    public partial class DispatchViewModel : SingleObjectViewModel<Dispatch, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of DispatchViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static DispatchViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new DispatchViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the DispatchViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the DispatchViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected DispatchViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Dispatches, x => x.DriverName)
        {
        }


        /// <summary>
		/// The view model that contains a look-up collection of CustmerMasters for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<CustmerMaster> LookUpCustmerMasters
        {
            get { return GetLookUpEntitiesViewModel((DispatchViewModel x) => x.LookUpCustmerMasters, x => x.CustmerMasters); }
        }

        /// <summary>
        /// The view model for the DispatchLabelPrintings detail collection.
        /// </summary>
        public CollectionViewModel<LabelPrinting, decimal, IMMPPLDBEntitiesUnitOfWork> DispatchLabelPrintingsDetails
        {
            get { return GetDetailsCollectionViewModel((DispatchViewModel x) => x.DispatchLabelPrintingsDetails, x => x.LabelPrintings, x => x.DptId, (x, key) => { x.DptId = key; }); }
        }
    }
}
