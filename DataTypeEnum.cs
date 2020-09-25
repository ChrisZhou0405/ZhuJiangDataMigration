using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhuJiangDataMigration
{
    public enum DataTypeEnum
    {
        //	数据类型(薪酬)(1:文本型 3:整数型 2:实数型 5:长日期型 4:短日期型 8:逻辑型 21:金额型 0:关联型 7:组织类型 13:基础类型 )
        关联型=0,
        文本型 =1,
        实数型 = 2,
        整数型 =3,
        短日期型 = 4,
        长日期型 = 5,
        组织类型=7,
        逻辑型 = 8,
        基础类型=13,
        金额型 = 21,


    }
}
