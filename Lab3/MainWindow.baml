   M S B A M L   `   `   `  ����?  ;Lab3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab3Lab3  5   	   H DLab3ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:Lab3ViewModel;assembly=Lab3ViewModelLab3ViewModel 6*       Lab3.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Lab3     5   	   Elocal12clr-namespace:Lab3ViewModel;assembly=Lab3ViewModel     6*     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	      *  $Lab3ViewModel.FirstElement_Converter   FirstElement_Converter8  2Lab3ViewModel.DataCollection_coordinates_Converter ( $DataCollection_coordinates_Converter  key_details_DataTemplate �� Orientation �� Path �� 	Converter��%T  &	        &	      &	 &     7    5
   
   6(     5   
   66   �� 5   
   .R�5      �� $ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��time:��6    �� 5      ��6    �� $ t�5      �� 5      $ 
Horizontal=�6   .+�5      �� ��6    �� 8   5      5      5      5      .��5      � -   5         �Name$ grid_collection��5       � RowDefinitions 5   
   �� 5      $
��113* �6   �� 5      $
��104* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5       $��1* �6   �� 5!      $��1* �6   .+�5#   
   �� -   5        $��ListBox_Main��5#      $��10,51,9.667,10.333q�6n   ��6&   �� *
Collection+ �� UpdateSourceTrigger$ PropertyChanged=�6n   o� 5$   
   $��0,0,10,0q�6   $	��Top=�6!   $��4��69   $��25��6M   .@�5%      m� $
��File��6   .@�5&      m� $	��New��6   	 m� Command	 6(   �� *
NewCommand+m� 5'      $
��Open��6   	 6*   �� *OpenCommand+m� 5(      $
��Save��6   	 6)   �� *SaveCommand+5)      m� 5*      $
��Edit��6   .@�5+      m� $��Add Defaults��6   	 61   �� *AddDefaultsCommand+m� 5,      $"��Add Default V1DataCollection��6   	 6A   �� *!AddDefaultDataCollectionCommand+m� 5-      $��Add Default V1DataOnGrid��6   	 6=   �� *AddDefaultDataOnGridCommand+m� 5.      $��Add Element from File��6   	 6;   �� *AddElementFromFileCommand+m� 5/      $��Remove��6   	 6+   �� *RemoveCommand+
 m� CommandParameter
 6M   ��  �� ElementName$ ListBox_Main��$ SelectedItems�50      51      �� 52   
   -   5        $��ListBox_DataCollection��52   C   $��1��6   $��10.333,51,10.333,10.333q�6"   ��6a   �� *DataCollection+$ PropertyChanged=��� 53   
   -   5        $��ListBox_DataOnGrid��53   B   $��2��6   $��9.667,51,10.333,10.333q�6"   ��6\   �� *
DataOnGrid+$ PropertyChanged=��� 54   
   -   5        $��Label_MaxCount��54   C   $
��Left=�6   $��128,21.667,0,0q�6+   $	��Top=�6�   $��37��6�    J� RenderTransformOrigin$ 0.541,0.566&�6�   $��1��6�   ��6Y   �� *
MaxCount+$ PropertyChanged=�6�   �� 55   
   -   5        $��ListBox_details��55   S   $��1��6   $��1��6"   $ ��10.333,35.667,10.333,9.667q�6/   #��[ 6�   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��6j   �� $ ListBox_DataCollection��$ SelectedItem.value�6�   � 56   
   -   5        $��Grid_DataOnGrid��56      $��9.667,35.667,10.333,9.667q�6&   $��2��6I   $��1��6Y   .+�57      �� -   5        $��TextBlock_FirstElement��57   c   $��2��6   $
��Left=�6(   $��	10,31,0,0q�6C   $��1��6V   $
��Wrap=�6�   $	��Top=�6�   ��6�   �� $ ListBox_DataOnGrid��$" SelectedItem.points_value[0]�6�   �� 58      -	   5        $��TextBlock_LastElement��58   c   $��2��6   $
��Left=�6(   $��	10,73,0,0q�6C   $��1��6V   $
��Wrap=�6  $	��Top=�6  ��6�   �� $ ListBox_DataOnGrid��$ SelectedItem.points_value�# [  6  �� 59      $��2��6   $
��Left=�6(   $��	10,10,0,0q�6C   $��1��6V   $ ��First element V1DataOnGrid��6c   $
��Wrap=�6�   $	��Top=�6�   �� 5:      $��2��6   $
��Left=�6(   $��	10,52,0,0q�6C   $��1��6V   $��Last element V1DataOnGrid��6c   $
��Wrap=�6�   $	��Top=�6�   5<      �� 5=   
   $
��Left=�6   $��10,10.667,0,0q�6/   $:��4Max count of nodes in elements of V1MainCollection :��6F   $
��Wrap=�6�   $	��Top=�6�   $	��179��6�   $��1��6�   �� 5>   
   $��3��6   $��Center=�6$   $��0,30,0,0q�6A   $��Add Custom V1DataOnGrid��6S   $
��Wrap=�6r   $	��Top=�6�   $	��150��6�   ���	#FFDD7B7B6�   �� 5?   
   $��3��6   $��Center=�6$   $��0,51,0,0q�6A   $2��,String базового класса V1Data:��6S   $
��Wrap=�6y   $	��Top=�6�   $	��180��6�   �� 5@   
   $��3��6   $��Center=�6$   $��0,90,0,0q�6A   $;��5Число узлов сетки по времени:��6S   $
��Wrap=�6x   $	��Top=�6�   $	��180��6�   $��16��6�   �� 5A   
   $��3��6   $��Center=�6$   $��	0,129,0,0q�6A   $��	minValue:��6T   $
��Wrap=�6e   $	��Top=�6y   $	��180��6�   �� 5B   
   $��3��6   $��Center=�6$   $��	0,168,0,0q�6A   $��	maxValue:��6T   $
��Wrap=�6e   $	��Top=�6y   $	��180��6�   � 5D   
   -
   5        $��GridAddCustom��5D      $��3��6$   .+�5E      �� -   5        $��TextBox_V1DataString��5E   F   $��Center=�6   $��0,67,0,0q�64    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��180��6�   ��6c   �� *
DataString+ �� ValidatesOnDataErrors . $ PropertyChanged=�6�   �� 5F      -   5        $��TextBox_GridNumber��5F   G   $��Center=�6   $��	0,106,0,0q�64   $
 Wrap=�6�   $	��Top=�6�   $	��180��6�   ��6a   �� *NumberOfGridPoints+ . $ PropertyChanged=�6�   �� 5G      -   5        $��TextBox_minValue��5G   G   $��Center=�6   $��	0,145,0,0q�64   $
 Wrap=�6�   $	��Top=�6�   $	��180��6�   ��6_   �� *
MinValue+ . 6�   �� 5H      -   5        $��TextBox_maxValue��5H   G   $��Center=�6   $��	0,184,0,0q�64   $
 Wrap=�6�   $	��Top=�6�   $	��180��6�   ��6_   �� *
MaxValue+ . 6�   5J      �� 5K   
   $��
Add Custom��6   $��3��6K   $
��Left=�6[   $��9.667,10.667,0,0q�6v   $��1��6�   $	��Top=�6�   $	��165��6�   ��6&   �� *AddCustomCommand+6�   �� 5L   
   $��3��6   $
��Left=�6$   $��35��6?   $��9.667,40.667,0,0q�6K   $��1��6e   $
��Wrap=�6r   $=��7-the string must be unique for each V1DataOnGrid object��6�   $	��Top=�6�   $	��178��6�   �� 5M   
   $��3��6   $
��Left=�6$   $��9.667,80.667,0,0q�6?   $��1��6Y   $
��Wrap=�6f   $>��8-the number of grid nodes in time must be greater than 2��6z   $	��Top=�6�   �� 5N   
   $��3��6   $
��Left=�6$   $��9.667,117.667,0,0q�6?   $��1��6Z   $
��Wrap=�6g   $8��2-the MinValue value must be less than the MaxValue��6{   $	��Top=�6�   �� 5O   
   $��Center=�6   $��60,30,43.667,0q�61   $
��Wrap=�6I   $��V1MainCollection��6]   $	��Top=�6u   $��95��6�   ���	#FFDD7B7B6�   �� 5P   
   $��1��6   $
��Left=�6$   $��59.333,30,0,0q�6?   $
��Wrap=�6V   $��V1DataCollection��6j   $	��Top=�6�   $ 0.507,0.646&�6�   ���	#FFDD7B7B6�   �� 5Q   
   $��2��6   $
��Left=�6$   $��62.667,30,0,0q�6?   $
��Wrap=�6V   $��V1DataOnGrid��6j   $	��Top=�6~   ���	#FFDD7B7B6�   �� 5R   
   $��1��6   $
��Left=�6$   $��10.333,5.667,0,0q�6?   $��1��6Y   $
��Wrap=�6f   $3��-Details of selected item in V1DataCollection:��6z   $	��Top=�6�   $	��178��6�   �� 5S   
   $��2��6   $
��Left=�6$   $��10.667,5.667,0,0q�6?   $��1��6Y   $
��Wrap=�6f   $/��)Details of selected item in V1DataOnGrid:��6z   $	��Top=�6�   $	��177��6�   5U      5V      