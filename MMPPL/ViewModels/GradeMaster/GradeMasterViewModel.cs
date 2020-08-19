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
    /// Represents the single GradeMaster object view model.
    /// </summary>
    public partial class GradeMasterViewModel : SingleObjectViewModel<GradeMaster, decimal, IMMPPLDBEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of GradeMasterViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static GradeMasterViewModel Create(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new GradeMasterViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the GradeMasterViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the GradeMasterViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected GradeMasterViewModel(IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.GradeMasters, x => x.GradeName)
        {
        }

        /// <summary>
        /// The view model for the GradeMasterLabelPrintings detail collection.
        /// </summary>
        public CollectionViewModel<LabelPrinting, decimal, IMMPPLDBEntitiesUnitOfWork> GradeMasterLabelPrintingsDetails
        {
            get { return GetDetailsCollectionViewModel((GradeMasterViewModel x) => x.GradeMasterLabelPrintingsDetails, x => x.LabelPrintings, x => x.GradeId, (x, key) => { x.GradeId = key; }); }
        }
    }
}
