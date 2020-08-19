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
    /// Represents the single ColorMaster object view model.
    /// </summary>
    public partial class ColorMasterViewModel : SingleObjectViewModel<ColorMaster, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of ColorMasterViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ColorMasterViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new ColorMasterViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the ColorMasterViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ColorMasterViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ColorMasterViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.ColorMasters, x => x.ColorName)
        {
        }

        /// <summary>
        /// The view model for the ColorMasterLabelPrintings detail collection.
        /// </summary>
        public CollectionViewModel<LabelPrinting, decimal, IMMPPLDBEntitiesUnitOfWork> ColorMasterLabelPrintingsDetails
        {
            get { return GetDetailsCollectionViewModel((ColorMasterViewModel x) => x.ColorMasterLabelPrintingsDetails, x => x.LabelPrintings, x => x.ColorId, (x, key) => { x.ColorId = key; }); }
        }
    }
}
