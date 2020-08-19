using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using MMPPL.Common.Utils;
using MMPPL.Common.DataModel;
using MMPPL.Common.DataModel.EntityFramework;
using MMPPL;
using DevExpress.Mvvm;
using System.Collections;
using System.ComponentModel;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Data.Async.Helpers;

namespace MMPPL.MMPPLDBEntitiesDataModel
{
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource
    {

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation.
        /// </summary>
        public static IUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork> GetUnitOfWorkFactory()
        {
            return new DbUnitOfWorkFactory<IMMPPLDBEntitiesUnitOfWork>(() => new MMPPLDBEntitiesUnitOfWork(() => new MMPPLDBEntities()));
        }
    }
}