   M S B A M L   `   `   `  ����Q  MTermoControllerMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TermoControllerMonitorTermoControllerMonitor  5   	   A =AGauge, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullK2clr-namespace:System.Windows.Forms;assembly=AGaugeSystem.Windows.Forms 5   	   *    $TermoControllerMonitor.MonitorWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:TermoControllerMonitor     5   	   Awf2clr-namespace:System.Windows.Forms;assembly=AGauge     5   	   -   5          � Title$/  )Мониторинг параметров��5	   	   $��440.572��6'   $��602.964��68   .��5
      � -   5         �Name$ 
MonitorGid��5
      $��0,0,2,3q�6    � ColumnDefinitions 5   
   �� 5      $
��210* �6   �� 5      $
��209* �6    � RowDefinitions 5   
   �� 5      $
��139* �6   �� 5      $
��197* �6   �� 5      $	��48* �6   .+�5   
   �� -   5         ��Name$ currentCondition��5      $
��Left=�6,   $��23��6G   $��	87,19,0,0q�6S    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $��30��6�   $��1��6�   �� 5   
   -   5         ��Name$ LabeltempOffHeater��5      $&�� Ток кондиционера:��6,   $
��Left=�6H   $	��Top=�6c   $��31��6{   $	��295��6�   $��2,17,0,0q�6�   �� 5   
   -   5        $ currentHeater��5      $
��Left=�6)   $��23��6D   $��	87,49,0,0q�6P   $
 Wrap=�6c   $	��Top=�6w   $��30��6�   $��1��6�   �� 5   
   -   5        $ LabeltempOffHeater_Copy��5      $$��Ток нагревателя:��61   $
��Left=�6L   $	��Top=�6g   $��31��6   $	��295��6�   $��2,47,0,0q�6�   �� 5   
   -   5        $ voltageHeater��5      $
��Left=�6)   $��23��6D   $��	87,80,0,0q�6P   $
 Wrap=�6c   $	��Top=�6w   $��30��6�   $��1��6�   �� 5   
   -   5        $ LabeltempOffHeater_Copy1��5      $2��,Напряжение нагревателя:��62   $
��Left=�6T   $	��Top=�6o   $��31��6�   $	��295��6�   $��2,78,0,0q�6�   7  1System.Windows.Forms.Integration.WindowsFormsHost  5   
   $��1��6   $��0,0,2,0q�6(      Child. 5      !  System.Windows.Forms.AGauge  -	   5        gAGaugeFanExt��5      5        5   
   $��1��6   $��1��6(   $��3,0,0,0q�68   . 5        -
   5        gAGaugeFanInt��5      5      �� 5    
   -   5        $ LabeltempOffHeater_Copy2��5       $)��#Внешний вентилятор��62   $
��Left=�6O   $��Center=�6j   $	��Top=�6�   $��31��6�   $	��225��6�   $��	10,10,0,0q�6�   $��2��6�   �� 5!   
   -   5        $ LabeltempOffHeater_Copy3��5!      $/��)Внутренний вентилятор��62   $
��Left=�6R   $��Center=�6m   $	��Top=�6�   $��31��6�   $	��234��6�   $��	10,10,0,0q�6�   $��2��6�   $��1��6�   5"      5#      