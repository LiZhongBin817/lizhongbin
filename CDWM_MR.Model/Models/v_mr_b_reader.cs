using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_mr_b_reader
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_mr_b_reader()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _mrreadername;
        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get { return this._mrreadername; } set { this._mrreadername = value; } }
    }
}
