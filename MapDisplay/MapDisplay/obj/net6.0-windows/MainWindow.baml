   M S B A M L   `   `   `  ���� E  AMapDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MapDisplay
MapDisplay      MapDisplay.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:MapDisplay       � Title$  
MainWindow��$	��450��$	��800��.���  � ColumnDefinitions �� $��5* ��� $
��Auto ��� $��* � � RowDefinitions �� $��* ��� $
��Auto �.+��� -   $��0��$��0��$��Visible=�$��Visible=�$
��True��
�������.���� -    ��Name$ HeightmapCanvas��.+��� -    ��Name$ HeightmapImage��$
��None=��� $��0��$��1��$��5��$��Stretch=�� $��0��$��2��$��5q� �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� -    ��Name$ RedCrossCoordinates��$��5q�$��14(�$��0���� $��Top 10 Highest Points��$
��Bold"�$��14(�$��5q�$��1��&   System.Windows.Controls.DataGrid  -     Name$ Top10HighestDataGrid��$��2��   AutoGenerateColumns$ False��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Point��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *Point+  $	 Height��
 �� *Height+�� $��Top 10 Lowest Points��$
��Bold"�$��14(�$��5q�$��3��  -   $ Top10LowestDataGrid��$��4��$ False��   $	 Point��
 �� *Point+  $	 Height��
 �� *Height+�� $��1��$��0�� �� Orientation$ 
Horizontal=�$��5q�.+��� $��Coordinates: ��$��Center=��� -    ��Name$ CoordinateLabel��$��X: 0, Y: 0, Height: 0��$��Center=��� -   $ OutputTextBlock��$��1��$��2��$��5q�