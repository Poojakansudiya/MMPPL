using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using MMPPL.Common.Utils;
using MMPPL.MMPPLDBEntitiesDataModel;
using MMPPL.Common.DataModel;
using MMPPL;
using MMPPL.Common.ViewModel;

namespace MMPPL.ViewModels
{
    /// <summary>
    /// Represents the CustmerMasters collection view model.
    /// </summary>
    public partial class CustmerMasterCollectionViewModel : CollectionViewModel<CustmerMaster, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of CustmerMasterCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CustmerMasterCollectionViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new CustmerMasterCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CustmerMasterCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CustmerMasterCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CustmerMasterCollectionViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.CustmerMasters)
        {
        }
    }
}