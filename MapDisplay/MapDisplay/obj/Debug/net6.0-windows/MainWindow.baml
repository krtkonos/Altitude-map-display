   M S B A M L   `   `   `  ����E  AMapDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MapDisplay
MapDisplay  5   	       MapDisplay.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MapDisplay     5   	     � Title$  
MainWindow��5   	   $	��720��6   $
��1280��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��5* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��0��5      $��0��6$   $��Visible=�5      $��Visible=�5      $
��True��5      
�������.��5      �� -   5         ��Name$ HeightmapCanvas��5      .+�5      �� -   5         ��Name$ HeightmapImage��5      $
��None=�60   6d   5      5      �� 5   
   $��0��6   $��1��6$   $��5��64   $��Stretch=�6>   � 5!   
   $��0��6   $��2��6   $��5q�6,    5"      �� 5#      $
��Auto �6    �� 5$      $
��Auto �6    �� 5%      $��* �6    �� 5&      $
��Auto �6    �� 5'      $��* �6    .+�5*      �� -   5         ��Name$ RedCrossCoordinates��5*      $��5q�65   $��14(�6@   $��0��6N   �� 5+      $��Top 10 Highest Points��6   $
��Bold"�65   $��14(�6G   $��5q�6U   $��1��6`   &   System.Windows.Controls.DataGrid  5,      -   5          Name$ Top10HighestDataGrid��5,      $��2��65      AutoGenerateColumns$ False��6B      Columns 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Point��6)   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 68   �� *Point+  50      $	 Height��6)   
 69   �� *Height+52      �� 53      $��Top 10 Lowest Points��6   $
��Bold"�64   $��14(�6F   $��5q�6T   $��3��6_     54      -   5        $ Top10LowestDataGrid��54      $��4��64   $ False��6A    55        56      $	 Point��6)   
 68   �� *Point+  57      $	 Height��6)   
 69   �� *Height+59      5:      �� 5<   
   $��1��6   $��0��6"    �� Orientation$ 
Horizontal=�62   $��5q�6K   .+�5=      �� $��Coordinates: ��6   $��Center=�6-   �� 5>      -   5         ��Name$ CoordinateLabel��5>      $��X: 0, Y: 0, Height: 0��6-   $��Center=�6M   �� 5?      -   5        $ OutputTextBlock��5?      $��1��61   $��2��6>   $��5q�6N   5A      5B      5C      