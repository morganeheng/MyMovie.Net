   M S B A M L   `   `   `  ����F  BMovieNetWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MovieNetWpfMovieNetWpf  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	       MovieNetWpf.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MovieNetWpf     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   f bGalaSoft.MvvmLight.Platform, Version=5.3.0.19032, Culture=neutral, PublicKeyToken=5f873c45e98af8a1*cmd http://www.galasoft.ch/mvvmlight  5	   	   -   5          �Name$  MainWindowName��5   	    � Title$ 
MainWindow��5   	   $	��456��6   $	��446��6)   ���	#FF252323$��10(�6b    � DataContext 5   	   ��    ViewModelLocator �� Source# [   �� Path$ MainVM���5   L   Z� .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      /  )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand . 	   Command	 6!   �� *OnClosingCommand+5   !   5      .��5      � ��5   
   �� 5      
 �� ImageSource$8
 2pack://application:,,,/Resources/movie-clapper.png��6   .+�5   
   �� $
��Left=�6   $
��Wrap=�6/   $��IDENTIFIANT��6C   $	��Top=�6V   $��
38,240,0,0q�6n   
�������6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��MOT DE PASSE��6C   $	��Top=�6W   $��
38,300,0,0q�6o   
�������6�   �� 5   
   -   5        $
��user��5   (   $
��Left=�64   $��20��6O    �� TextWrapping$
 Wrap=�6[   $	��Top=�6o   $	��268��6�   $��135,238,0,0q�6�   
�������
���   ���6   �� *User+6�   G� 5   
   -   5        $
��pass��5   �   &    MovieNetWpf.PasswordBoxAssistant   BindPassword . $
��Left=�6�   $	��Top=�6�   $	��268��6�   $��135,298,0,0q�6�   $��20��6�   
���   �
�������
�������   BoundPassword 6F   �� $ Password� �� Mode$ TwoWay=�6(  �� 5   
   $��CONNECT��6   $
��Left=�6~   $	��Top=�6�   $��75��6�   $��328,368,0,0q�6�   
���   �
�������
���������6#   �� *CommandConnect+��6F   ��  �� ElementName$ MainWindowName��6�   �� 5   
   $��REGISTER��6   $
��Left=�6�   $	��Top=�6�   $��75��6�   $��
42,368,0,0q�6�   
�������
�������
���   ���6$   �� *CommandRegister+��6H   �� $ MainWindowName��6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��	MOVIE NET��6C   $	��Top=�6T   $��
42,173,0,0q�6l   
�������$	��361��6�    �� TextAlignment$ Center=�6�   $��20(�6�   $
��Bold"�6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $	��Top=�6C   $��193,364,0,0q�6[   
�������$��33��6�   $ Center=�6�   $��Italic$�6�   �� 6�   $�theoph_m��6�   �� 6�   �� 6�   $�heng_m��6�   6�   5      5       