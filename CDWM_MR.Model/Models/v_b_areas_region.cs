using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_b_areas_region
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_b_areas_region()
        {
        }

        private System.String _areano;
        /// <summary>
        /// 小区编号
        /// </summary>
        public System.String areano { get { return this._areano; } set { this._areano = value; } }

        private System.String _areaname;
        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get { return this._areaname; } set { this._areaname = value; } }

        private System.String _regionno;
        /// <summary>
        /// 所属片区编号
        /// </summary>
        public System.String regionno { get { return this._regionno; } set { this._regionno = value; } }

        private System.Int16? _areastate;
        /// <summary>
        /// 状态(1:可用;2:注销)
        /// </summary>
        public System.Int16? areastate { get { return this._areastate; } set { this._areastate = value; } }

        private System.String _regionname;
        /// <summary>
        /// 片区名称
        /// </summary>
        public System.String regionname { get { return this._regionname; } set { this._regionname = value; } }
    }
}