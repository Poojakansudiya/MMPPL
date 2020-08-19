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
    /// Represents the single SizeMaster object view model.
    /// </summary>
    public partial class SizeMasterViewModel : SingleObjectViewModel<SizeMaster, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of SizeMasterViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static SizeMasterViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new SizeMasterViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the SizeMasterViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the SizeMasterViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected SizeMasterViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.SizeMasters, x => x.SizeName)
        {
        }

        /// <summary>
        /// The view model for the SizeMasterLabelPrintings detail collection.
        /// </summary>
        public CollectionViewModel<LabelPrinting, decimal, IMMPPLDBEntitiesUnitOfWork> SizeMasterLabelPrintingsDetails
        {
            get { return GetDetailsCollectionViewModel((SizeMasterViewModel x) => x.SizeMasterLabelPrintingsDetails, x => x.LabelPrintings, x => x.SizeId, (x, key) => { x.SizeId = key; }); }
        }
    }
}
