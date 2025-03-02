object FMain: TFMain
  Left = 0
  Top = 0
  Width = 798
  Height = 549
  BorderIcons = [biSystemMenu]
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 24
    Top = 19
    Width = 105
    Height = 13
    Caption = #1042#1099#1073#1077#1088#1080#1090#1077' '#1076#1077#1081#1089#1090#1074#1080#1077':'
  end
  object Label3: TLabel
    Left = 140
    Top = 434
    Width = 108
    Height = 13
    Caption = #1048#1089#1093#1086#1076#1085#1086#1077' '#1089#1086#1086#1073#1097#1077#1085#1080#1077
  end
  object Label4: TLabel
    Left = 184
    Top = 486
    Width = 394
    Height = 13
    Caption = 
      #1042#1085#1080#1084#1072#1085#1080#1077'! '#1045#1089#1083#1080' '#1092#1072#1081#1083' '#1073#1086#1083#1100#1096#1086#1075#1086' '#1088#1072#1084#1077#1088#1072', '#1090#1086' '#1086#1090#1088#1072#1078#1072#1077#1090#1089#1103' '#1090#1086#1083#1100#1082#1086' '#1095#1072#1089#1090#1100' ' +
      #1090#1077#1082#1089#1090#1072'!'
  end
  object Label7: TLabel
    Left = 544
    Top = 167
    Width = 79
    Height = 13
    Caption = #1061#1077#1096' '#1089#1086#1086#1073#1097#1077#1085#1080#1103
  end
  object Label8: TLabel
    Left = 249
    Top = 61
    Width = 24
    Height = 18
    Caption = 'q ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label9: TLabel
    Left = 250
    Top = 28
    Width = 24
    Height = 18
    Caption = 'p ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 385
    Top = 30
    Width = 23
    Height = 18
    Caption = 'k ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label10: TLabel
    Left = 385
    Top = 61
    Width = 24
    Height = 18
    Caption = 'x ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label6: TLabel
    Left = 568
    Top = 249
    Width = 22
    Height = 13
    Caption = #1069#1062#1055
  end
  object Label11: TLabel
    Left = 582
    Top = 415
    Width = 76
    Height = 13
    Caption = #1055#1086#1083#1077' '#1087#1088#1086#1074#1077#1088#1082#1080
  end
  object Label12: TLabel
    Left = 400
    Top = 331
    Width = 73
    Height = 13
    Caption = #1042#1099#1073#1088#1072#1090#1100' '#1092#1072#1081#1083
  end
  object Label13: TLabel
    Left = 399
    Top = 345
    Width = 74
    Height = 13
    Caption = #1076#1083#1103' '#1087#1088#1086#1074#1077#1088#1082#1080':'
  end
  object Label5: TLabel
    Left = 400
    Top = 284
    Width = 24
    Height = 18
    Caption = 'y ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label14: TLabel
    Left = 24
    Top = 76
    Width = 24
    Height = 18
    Caption = 'h ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -15
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object ComboBox1: TComboBox
    Left = 24
    Top = 46
    Width = 177
    Height = 21
    ItemHeight = 13
    TabOrder = 0
    Text = #1055#1086#1076#1087#1080#1089#1072#1090#1100
    OnChange = ComboBox1Change
    OnKeyPress = ComboBox1KeyPress
    Items.Strings = (
      #1055#1086#1076#1087#1080#1089#1072#1090#1100
      #1055#1088#1086#1074#1077#1088#1080#1090#1100)
  end
  object Edit1: TEdit
    Left = 280
    Top = 29
    Width = 68
    Height = 21
    TabOrder = 1
    OnKeyPress = Edit1KeyPress
  end
  object Button1: TButton
    Left = 84
    Top = 460
    Width = 82
    Height = 39
    Caption = #1054#1090#1082#1088#1099#1090#1100'...'
    TabOrder = 2
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 512
    Top = 36
    Width = 201
    Height = 41
    Caption = #1053#1072#1095#1072#1090#1100
    TabOrder = 3
    OnClick = Button2Click
  end
  object Memo1: TMemo
    Left = 24
    Top = 120
    Width = 345
    Height = 308
    Lines.Strings = (
      'Memo1')
    TabOrder = 4
  end
  object Memo3: TMemo
    Left = 400
    Top = 186
    Width = 346
    Height = 57
    Lines.Strings = (
      'Memo1')
    TabOrder = 5
  end
  object Button3: TButton
    Left = 595
    Top = 462
    Width = 82
    Height = 37
    Caption = #1057#1086#1093#1088#1072#1085#1080#1090#1100' '#1074'...'
    TabOrder = 6
    OnClick = Button3Click
  end
  object Edit2: TEdit
    Left = 279
    Top = 62
    Width = 68
    Height = 21
    TabOrder = 7
    OnKeyPress = Edit1KeyPress
  end
  object Edit3: TEdit
    Left = 415
    Top = 29
    Width = 68
    Height = 21
    TabOrder = 8
    OnKeyPress = Edit1KeyPress
  end
  object Edit4: TEdit
    Left = 415
    Top = 62
    Width = 68
    Height = 21
    TabOrder = 9
    OnKeyPress = Edit1KeyPress
  end
  object Memo2: TMemo
    Left = 400
    Top = 120
    Width = 346
    Height = 41
    Lines.Strings = (
      'Memo1')
    TabOrder = 10
  end
  object Memo4: TMemo
    Left = 496
    Top = 328
    Width = 245
    Height = 81
    Lines.Strings = (
      'Memo1')
    TabOrder = 11
  end
  object Button4: TButton
    Left = 399
    Top = 365
    Width = 82
    Height = 37
    Caption = #1042#1099#1073#1088#1072#1090#1100
    TabOrder = 12
    OnClick = Button4Click
  end
  object Edit5: TEdit
    Left = 430
    Top = 285
    Width = 311
    Height = 21
    Enabled = False
    TabOrder = 13
    OnKeyPress = Edit1KeyPress
  end
  object Edit6: TEdit
    Left = 54
    Top = 76
    Width = 68
    Height = 21
    TabOrder = 14
    OnKeyPress = Edit1KeyPress
  end
  object OpenDialog1: TOpenDialog
    Top = 488
  end
  object SaveDialog1: TSaveDialog
    Left = 32
    Top = 488
  end
  object OpenDialog2: TOpenDialog
    Left = 64
    Top = 488
  end
end
