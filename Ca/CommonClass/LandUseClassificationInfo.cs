using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ca.CommonClass
{
    /// <summary>
    /// 记录栅格图像数值与类型的对应关系
    /// </summary>
    public class LandUseClassificationInfo
    {


        #region fields

        /// <summary>
        /// 空值类型信息
        /// </summary>
        private StructLanduseInfo nullInfo = null;

        /// <summary>
        /// 城市类型信息
        /// </summary>
        private List<StructLanduseInfo> urbanInfos = null;

        /// <summary>
        /// 可以转化为城市类型信息
        /// </summary>
        private List<StructLanduseInfo> convertableInfos = null;

        /// <summary>
        /// 不可以转化为城市类型信息
        /// </summary>
        private List<StructLanduseInfo> unConvertableInfos = null;

        /// <summary>
        /// 所有的土地利用类型信息。
        /// </summary>
        private List<StructLanduseInfo> allTypes = null;

        #endregion

        #region properties

        // 土地利用类型的数目
        public int NumOfLandUseTypes
        {
            get
            {
                if (this.allTypes != null)
                {
                    return this.allTypes.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        // 空信息
        public StructLanduseInfo NullInfo
        {
            get
            {
                return nullInfo;
            }
            set
            {
                nullInfo = value;

            }
        }

        // 水体
        public StructLanduseInfo WaterInfo
        {
            get
            {
                foreach(var type in AllTypes)
                {
                    if(type.LandUseTypeName == "水体")
                    {
                        return type;
                    }
                }
                return null;
            }
        }

        // 所有土地利用类型信息
        public List<StructLanduseInfo> AllTypes
        {
            get
            {
                if (allTypes == null)
                {
                    this.allTypes = new List<StructLanduseInfo>();                    
                }
                return this.allTypes;
            }
            set
            {
                this.allTypes = value;
            }
        }

        /// <summary>
        /// 城市类型信息
        /// </summary>
        public List<StructLanduseInfo> UrbanInfos
        {
            get
            {
                if (this.urbanInfos == null)
                {
                    this.urbanInfos = new List<StructLanduseInfo>();
                }
                return this.urbanInfos;
            }
            set
            {
                this.urbanInfos = value;
            }
        }

        /// <summary>
        /// 不可以转化为城市类型信息
        /// </summary>
        public List<StructLanduseInfo> UnConvertableInfos
        {
            get
            {
                if (this.unConvertableInfos == null)
                {
                    this.unConvertableInfos = new List<StructLanduseInfo>();
                }
                return this.unConvertableInfos;
            }
            set
            {
                this.unConvertableInfos = value;
            }
        }

        /// <summary>
        /// 可以转化为城市类型信息
        /// </summary>
        public List<StructLanduseInfo> ConvertableInfos
        {
            get
            {
                if (this.convertableInfos == null)
                {
                    this.convertableInfos = new List<StructLanduseInfo>();
                }
                return this.convertableInfos;
            }
            set
            {
                this.convertableInfos = value;
            }
        }

        public int UrbanIndex
        {
            get
            {
                // 调高城市发展概率
                for (int m = 0; m < this.AllTypes.Count; m++)
                {
                    if (this.UrbanInfos[0].LandUseTypeValue == this.AllTypes[m].LandUseTypeValue)
                    {
                        return m;
                    }
                }
                return 0;
            }
        }


        #endregion




        #region public methods

        /// <summary>
        /// 判断一个栅格数值是否是城市
        /// </summary>
        /// <param name="val">栅格数值</param>
        /// <returns></returns>
        public bool IsExistInUrbanInfos(double val)
        {
            for (int i = 0; i < this.UrbanInfos.Count; i++)
            {
                if (this.urbanInfos[i].LandUseTypeValue == val)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断一个栅格数值是否是可转化为城市的数值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool IsExistInConvertableInfos(double val)
        {
            for (int i = 0; i < this.ConvertableInfos.Count; i++)
            {
                if (this.ConvertableInfos[i].LandUseTypeValue == val)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断一个栅格数值是否是不可转化为城市数值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool IsExistInUnConvertableInfos(double val)
        {
            for (int i = 0; i < this.ConvertableInfos.Count; i++)
            {
                if (this.ConvertableInfos[i].LandUseTypeValue == val)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion


    }


}
