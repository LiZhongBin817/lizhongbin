using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
   public class v_interface
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_interface()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String InterfaceUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String OperationVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String InterfaceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int16? ExternalInterface { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int16? Verify { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public System.Int32 menuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String MenuName { get; set; }
    }
}
