   M S B A M L   `   `   `  ����F  BMovieNetWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MovieNetWpf.ViewMovieNetWpf.View  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	   !    MovieNetWpf.View.MenuWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MovieNetWpf.View     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   f bGalaSoft.MvvmLight.Platform, Version=5.3.0.19032, Culture=neutral, PublicKeyToken=5f873c45e98af8a1*cmd http://www.galasoft.ch/mvvmlight  5	   	   -   5          �Name$  MenuWindowName��5   	    � Title$ 
MenuWindow��5   	   $��458.667��6   $	��707��6-   ���	#FF252323 � DataContext 5   	   ��    ViewModelLocator �� Source# [   �� Path$ MenuVM���5   P   Z� .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      /  )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand . 	   Command	 6!   �� *OnClosingCommand+5   !   5      .��5      � 
 � ColumnDefinitions
 5   
   �� 5      $
��151* �6   �� 5      $	��89* �6   ��5   
   �� 5       �� ImageSource$4 .pack://application:,,,/Resources/film_roll.png��6    j� Stretch$ Uniform=�6V   .+�5   
   �� $!��AJOUTER 
     UN
    FILM��6   $
��Left=�6f   $��
64,116,0,0q�6�   $	��Top=�6�   $��58��6�   $��55��6�   $
��Bold"�6�   ��6G   �� *
CommandAdd+��6�   Z� ��6�   Z� 6�   �� 5   
   $$��MODIFIER 
      UN
     FILM��6   $
��Left=�6l   $��294,254,0,0q�6�   $	��Top=�6�   $��72��6�   $��58��6�   $
��Bold"�6�   ��6J   �� *CommandUpdate+��6�   Z� ��6�   Z� 6�   �� 5   
   $&�� SUPPRIMER
       UN
      FILM��6   $
��Left=�6n   $��292,116,0,0q�6�   $	��Top=�6�   $��75��6�   $��55��6�   $
��Bold"�6�   ��6L   �� *CommandDelete+��6�   Z� ��6�   Z� 6�   �� 5   
   $��LISTE 
 DES 
FILMS��6   $
��Left=�6V   $��
191,47,0,0q�6q   $	��Top=�6�   $��43��6�   $��51��6�   $
��Bold"�6�   ��66   �� *CommandList+��6�   Z� ��6�   Z� 6�   �� 5   
   $'��!RECHERCHER
        UN
       FILM��6   $
��Left=�6g   $��
53,253,0,0q�6�   $	��Top=�6�   $��79��6�   $��61��6�   $
��Bold"�6�   ��6E   �� *CommandSearch+��6�   Z� ��6�   Z� 6�   �� 5    
   $�� MON
PROFIL��6   $
��Left=�6R   $��185,328,0,0q�6m   $	��Top=�6�   $��52��6�   $��40��6�   $
��Bold"�6�   ��60   �� *CommandProfil+��6�   Z� ��6�   Z� 6�   �� 5!   
   $��LOGOUT��6   $
��Left=�6|   $��186,200,0,0q�6�   $	��Top=�6�   $��51��6�   $��22��6�   $
��Bold"�6�   $��11(�6  ��6"   �� *CommandLogout+��6D   ��  �� ElementName$ MenuWindowName����6�   Z� ��6  Z� �� 5"   
   $��1��6   $
��Left=�6$   $��	0,116,0,0q�6?   $
��Wrap=�6R   $	��Top=�6f   $	��200��6~   $	��233��6�   
�������$
��Bold"�6�    �� TextAlignment$ Center=�6�   $��20(�6�   ��6�   �� *Msg+6�   5#      5$      