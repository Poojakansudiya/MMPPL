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
    /// Represents the single MicronMaster object view model.
    /// </summary>
    public partial class MicronMasterViewModel : SingleObjectViewModel<MicronMaster, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of MicronMasterViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static MicronMasterViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new MicronMasterViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the MicronMasterViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the MicronMasterViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected MicronMasterViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.MicronMasters, x => x.MicronName)
        {
        }

        /// <summary>
        /// The view model for the MicronMasterLabelPrintings detail collection.
        /// </summary>
        public CollectionViewModel<LabelPrinting, decimal, IMMPPLDBEntitiesUnitOfWork> MicronMasterLabelPrintingsDetails
        {
            get { return GetDetailsCollectionViewModel((MicronMasterViewModel x) => x.MicronMasterLabelPrintingsDetails, x => x.LabelPrintings, x => x.MicronId, (x, key) => { x.MicronId = key; }); }
        }
    }
}
