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
    /// Represents the MicronMasters collection view model.
    /// </summary>
    public partial class MicronMasterCollectionViewModel : CollectionViewModel<MicronMaster, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of MicronMasterCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static MicronMasterCollectionViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new MicronMasterCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the MicronMasterCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the MicronMasterCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected MicronMasterCollectionViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.MicronMasters)
        {
        }
    }
}