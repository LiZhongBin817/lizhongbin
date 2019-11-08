namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class sys_interface_info
    {
        /// <summary>
        /// 
        /// </summary>
        public sys_interface_info()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID { get; set; }

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
    }
}
