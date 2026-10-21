# WPF小畫家

---

### 1. WPF 主視窗介面佈局（Layout Container）

* **DockPanel 應用**：作為視窗主要架構，將選單與工具列固定於上方（`DockPanel.Dock="Top"`）、狀態列固定於下方（`DockPanel.Dock="Bottom"`），並由 Canvas 自動填滿中間繪圖區域。
* **Canvas 繪圖畫布**：作為放置與繪製圖形元件的容器，提供絕對座標與繪圖環境。

---

### 2. 視窗選單與控制項選單列 (Menu, ToolBar & StatusBar)

* **Menu (功能選單)**：建立傳統下拉式選單（如「檔案」下的「開啟新畫布」、「儲存畫布」）。
* **ToolBarTray & ToolBar (工具列)**：收納常用功能，如色彩選擇器（ColorPicker）、筆刷粗細滑桿（Slider）以及形狀切換按鈕。
* **StatusBar (狀態列)**：視窗底部區域，包含多個 `StatusBarItem` 與 `Separator`，用於即時顯示當前選取的筆刷色彩、填滿色彩、筆刷粗細與繪圖形狀。

---

### 3. 第三方套件引進 (Extended WPF Toolkit)

* **NuGet 套件整合**：經由引入 `Xceed.Wpf.Toolkit` 命名空間（`xmlns:xctk="..."`）使用原生 WPF 欠缺的高級控制項。
* **xctk:ColorPicker 應用**：使用 `ColorPicker` 提供使用者選取筆刷與填滿色彩，並透過 `SelectedColorChanged` 事件捕捉顏色變更。

---

### 4. WPF UI 控制項與資料繫結 (Data Binding)

* **RadioButton 與 Tag 屬性**：透過 `RadioButton` 群組搭配 `Tag` 屬性（如 `"Line"`, `"Rectangle"`, `"Ellipse"`, `"Polyline"`）來切換繪圖模式。
* **Slider (滑桿)**：控制筆刷粗細（範圍 1~10），搭配 `IsSnapToTickEnabled` 進行數值調整。
* **XAML ElementName 資料繫結**：在 Label 中直接透過 `{Binding Value, ElementName=ThicknessSlider}` 動態顯示目前滑桿的數值。

---

### 5. C# 事件處理與狀態同步 (Code-Behind 邏輯)

* **事件觸發 (Event Handling)**：實作 `SelectedColorChanged`、`ValueChanged` 以及 `Checked` 事件處理函式。
* **類型轉換 (Type Casting)**：使用 `sender as RadioButton` 取得觸發元件並讀取 `Tag` 屬性。
* **狀態即時更新 (DisplayStatus)**：透過共用函式更新 `ColorLabel` 與 `ShapeLabel` 的顯示文字，將變數狀態同步呈現於 UI 狀態列。


## Ver2

---

### 一、 介面與控制項擴充 (XAML)

1. **整合 Extended WPF Toolkit (`xctk:ColorPicker`)**

* **舊版**：通常使用固定顏色選單或原生控制項設定單一顏色。
* **新版**：引入第三方套件 `Extended WPF Toolkit`，加入了 **筆刷色彩 (`StrokeColorPicker`)** 與 **填滿色彩 (`FillColorPicker`)** 兩個色彩選擇器。


2. **新增工具列 (ToolBar) 與按鈕列**

* **舊版**：基礎的按鈕或單一功能列。
* **新版**：使用 `ToolBarTray` 劃分為兩大工具列：

* **樣式工具列**：包含顏色選擇器、粗細滑桿 (`ThicknessSlider`) 與粗細數值綁定顯示 (`{Binding Value, ElementName=ThicknessSlider}`)。
* **繪圖與工具選單**：包含形狀選取按鈕（`Line`、`Rectangle`、`Ellipse`、`Polyline`）、**橡皮擦 (`EraserButton`)** 與 **清除畫布 (`ClearButton`)**。

3. **狀態列 (StatusBar) 資訊豐富化**

* 新增了三個區域的分隔狀態列：

* **`StatusLabel`**：動態顯示當前工作模式與座標。
* **`ShapeLabel`**：顯示目前選擇的形狀。
* **`ColorLabel`**：即時呈現筆刷色彩、填充色彩與筆刷粗細。







---

### 二、 功能與繪圖邏輯演進 (C# Code-Behind)

1. **支援多種繪圖形狀與動態預覽 (`Draw` 模式)**

* **舊版**：多為單一線條（`Line`）或固定圖形的畫筆。
* **新版**：
* **支援 4 種圖形**：直線 (`Line`)、矩形 (`Rectangle`)、橢圓 (`Ellipse`) 與多折線 (`Polyline`)。


* **即時拖曳預覽 (Rubber-banding)**：在 `MouseMove` 時，程式會先繪製臨時預覽圖形（灰色邊框/預設填滿），當滑鼠按鍵釋放 (`MouseLeftButtonUp`) 時，才會套用使用者設定的實際顏色與粗細。


* **正向座標計算**：使用 `Math.Min` 與 `Math.Abs` 確保向左或向上拖曳時也能正確計算矩形與橢圓的寬高與左上角原點。






2. **新增橡皮擦功能 (`Eraser` 模式)**

* **舊版**：只能繪製無法擦除。
* **新版**：切換至橡皮擦模式後，游標會變更為手勢 (`Cursors.Hand`)，滑鼠移過圖形時，會透過 `e.OriginalSource as Shape` 自動將被觸碰到的圖形物件從 `MyCanvas.Children` 中移除。




3. **新增全畫布清除功能 (`Clear` 模式)**

* 點擊清除按鈕會直接執行 `MyCanvas.Children.Clear()` 一鍵清空所有圖形。




4. **動態游標與狀態同步**

* 滑鼠進入畫布 (`MouseEnter`) 或按下時，會自動切換游標樣式（如 `Cursors.Pen`、`Cursors.Cross`、`Cursors.Hand`），提升使用者體驗。


* 透過 `DisplayStatus()` 共用函式，隨時將選取的色彩、粗細、形狀與座標同步更新至底部的狀態列。