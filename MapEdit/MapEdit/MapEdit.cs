using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MapEdit
{
    //
    public class MapEdit
    {
        private readonly MapData mapData;
        private readonly MapWriteScene mapWriteScene;
        private readonly MapShowArea mapShowArea;
        private readonly MapWriteScroll mapWriteScroll;
        private readonly EditMapChip editMapChip;
        private readonly SelectImageForm sif;
        public MapEdit(Panel mwp, MapChipResourceManager mcrm,SelectImageForm sif,ComboBox layerComboBox,HScrollBar hScroll,VScrollBar vScroll,Size MapSize,int mapChipSize)
        {
            this.sif = sif;
            this.mapData = new MapData(mcrm, MapSize, mapChipSize);
            mapWriteScene =
                new MapWriteScene(mwp, mapChipSize);
            mapShowArea = new MapShowArea(mapWriteScene, mapData);
            mapShowArea.UpdateShowMapImage();
            editMapChip = new EditMapChip(mapData, sif, mapWriteScene, layerComboBox);
            mapWriteScroll = new MapWriteScroll(hScroll, vScroll, mapWriteScene, mapData, mapShowArea);
            DXEX.DirectorForForm.AddSubScene(mapWriteScene);
        }

        public MapEdit(MapInfoFromText mift,Panel mwp, MapChipResourceManager mcrm, SelectImageForm sif, ComboBox layerComboBox, HScrollBar hScroll, VScrollBar vScroll):
            this(mwp,mcrm,sif,layerComboBox,hScroll,vScroll, mift.MapSize, mift.MapChipSize)
        {
            mapData.LoadProject(mift);
        }

        public void Dispose()
        {
            DXEX.DirectorForForm.RemoveSubScene(mapWriteScene);
            mapWriteScene.Dispose();
        }
        //マップの右回転
        public void MapRotateRight()
        {
            mapData.RotateRight();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum();
            //表示するスプライトの設定
            mapShowArea.UpdateShowMapImage();
        }

        //マップの左回転
        public void MapRotateLeft()
        {
            mapData.RotateLeft();
            //スクロールバーの調整
            mapWriteScroll.SetScrollMaximum();
            //表示するスプライトの設定
            mapShowArea.UpdateShowMapImage();
        }

        //マップの上下反転
        public void MapTurnVertical()
        {
            mapData.turnVertical();
            mapShowArea.UpdateShowMapImage();
        }

        //マップの左右反転
        public void MapTurnHorizontal()
        {
            mapData.turnHorizontal();
            mapShowArea.UpdateShowMapImage();
        }

        public void UpdateScrollValue()
        {
            mapWriteScroll.UpdateValue();
        }

        //キーによるスクロール
        public void KeyScroll(KeyEventArgs e)
        {
            mapWriteScroll.KeyScroll(e);
        }

        //マップ描画のパネルのサイズが変更されたら、
        //マップに表示するスプライトの調整や
        //スクロールバーの調整を行う
        public void mapWritePanel_SizeChanged()
        {
            mapWriteScroll.SetScrollMaximum();
            mapShowArea.UpdateShowMapImage();
        }

        //パネル上でマウスが操作された時の処理をする
        public void MapMouseAction(MouseEventArgs e)
        {
            editMapChip.MouseAction(e);
        }

        public void gridOnOff()
        {
            if (mapWriteScene.MapGrid.DrawFlag)
            {
                sif.mps.MapGrid.DrawDisable();
                mapWriteScene.MapGrid.DrawDisable();
            }
            else
            {
                sif.mps.MapGrid.DrawEnable();
                mapWriteScene.MapGrid.DrawEnable();
            }
        }

        public Bitmap GetBitmap()
        {
            return mapData.GetBitmap();
        }

        public StringBuilder GetMapDataText()
        {
            return mapData.GetMapDataText();
        }

        public ConfigForm CreateConfigForm()
        {
            return new ConfigForm(mapWriteScene, mapData, mapWriteScroll, mapShowArea);
        }
    }
}
