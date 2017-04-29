using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DXEX.Base
{
/*簡易エラー出力
 * Debugモードでのみコンパイルされる
 *flagで受け取ったデリゲートがtrueならエラーメッセージ表示 */
 //将来的には廃止
 //だってすごい使いずらいもん！！
    static class DebugMessage
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Mes(string errorMessage, bool flag)
        {
            if (flag)
            {
                var result = MessageBox.Show(errorMessage, "お取り扱いエラー", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel) DX.DxLib_End();
            }
        }
    }
}
