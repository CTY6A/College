/**********************************************************************/
/*   ____  ____                                                       */
/*  /   /\/   /                                                       */
/* /___/  \  /                                                        */
/* \   \   \/                                                       */
/*  \   \        Copyright (c) 2003-2009 Xilinx, Inc.                */
/*  /   /          All Right Reserved.                                 */
/* /---/   /\                                                         */
/* \   \  /  \                                                      */
/*  \___\/\___\                                                    */
/***********************************************************************/

/* This file is designed for use with ISim build 0x7708f090 */

#define XSI_HIDE_SYMBOL_SPEC true
#include "xsi.h"
#include <memory.h>
#ifdef __GNUC__
#include <stdlib.h>
#else
#include <malloc.h>
#define alloca _alloca
#endif
static const char *ng0 = "C:/Users/User/Desktop/LabsPOCP/Lab2_sum/Test_all_sum.vhd";
extern char *IEEE_P_2592010699;

unsigned char ieee_p_2592010699_sub_1690584930_503743352(char *, unsigned char );
unsigned char ieee_p_2592010699_sub_2507238156_503743352(char *, unsigned char , unsigned char );
unsigned char ieee_p_2592010699_sub_2545490612_503743352(char *, unsigned char , unsigned char );


static void work_a_2505427919_2372691052_p_0(char *t0)
{
    char *t1;
    char *t2;
    int64 t3;
    char *t4;
    unsigned char t5;
    char *t6;
    unsigned char t7;
    unsigned char t8;
    char *t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    unsigned char t14;
    char *t15;
    unsigned char t16;
    char *t17;
    unsigned char t18;
    unsigned char t19;
    unsigned char t20;
    char *t21;
    char *t22;
    char *t23;
    char *t24;

LAB0:    t1 = (t0 + 3952U);
    t2 = *((char **)t1);
    if (t2 == 0)
        goto LAB2;

LAB3:    goto *t2;

LAB2:    xsi_set_current_line(117, ng0);
    t3 = (25 * 1000LL);
    t2 = (t0 + 3760);
    xsi_process_wait(t2, t3);

LAB6:    *((char **)t1) = &&LAB7;

LAB1:    return;
LAB4:    xsi_set_current_line(118, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t2 = (t0 + 2312U);
    t6 = *((char **)t2);
    t7 = *((unsigned char *)t6);
    t8 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t5, t7);
    t2 = (t0 + 1992U);
    t9 = *((char **)t2);
    t10 = *((unsigned char *)t9);
    t2 = (t0 + 2472U);
    t11 = *((char **)t2);
    t12 = *((unsigned char *)t11);
    t13 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t10, t12);
    t14 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t8, t13);
    t2 = (t0 + 2152U);
    t15 = *((char **)t2);
    t16 = *((unsigned char *)t15);
    t2 = (t0 + 2632U);
    t17 = *((char **)t2);
    t18 = *((unsigned char *)t17);
    t19 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t16, t18);
    t20 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t14, t19);
    t2 = (t0 + 4336);
    t21 = (t2 + 56U);
    t22 = *((char **)t21);
    t23 = (t22 + 56U);
    t24 = *((char **)t23);
    *((unsigned char *)t24) = t20;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(120, ng0);
    t3 = (25 * 1000LL);
    t2 = (t0 + 3760);
    xsi_process_wait(t2, t3);

LAB10:    *((char **)t1) = &&LAB11;
    goto LAB1;

LAB5:    goto LAB4;

LAB7:    goto LAB5;

LAB8:    xsi_set_current_line(121, ng0);
    t2 = (t0 + 1192U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t7 = ieee_p_2592010699_sub_1690584930_503743352(IEEE_P_2592010699, t5);
    t2 = (t0 + 4400);
    t6 = (t2 + 56U);
    t9 = *((char **)t6);
    t11 = (t9 + 56U);
    t15 = *((char **)t11);
    *((unsigned char *)t15) = t7;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(122, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t2 = (t0 + 2312U);
    t6 = *((char **)t2);
    t7 = *((unsigned char *)t6);
    t8 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t5, t7);
    t2 = (t0 + 1992U);
    t9 = *((char **)t2);
    t10 = *((unsigned char *)t9);
    t2 = (t0 + 2472U);
    t11 = *((char **)t2);
    t12 = *((unsigned char *)t11);
    t13 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t10, t12);
    t14 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t8, t13);
    t2 = (t0 + 2152U);
    t15 = *((char **)t2);
    t16 = *((unsigned char *)t15);
    t2 = (t0 + 2632U);
    t17 = *((char **)t2);
    t18 = *((unsigned char *)t17);
    t19 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t16, t18);
    t20 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t14, t19);
    t2 = (t0 + 4336);
    t21 = (t2 + 56U);
    t22 = *((char **)t21);
    t23 = (t22 + 56U);
    t24 = *((char **)t23);
    *((unsigned char *)t24) = t20;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(124, ng0);
    t3 = (25 * 1000LL);
    t2 = (t0 + 3760);
    xsi_process_wait(t2, t3);

LAB14:    *((char **)t1) = &&LAB15;
    goto LAB1;

LAB9:    goto LAB8;

LAB11:    goto LAB9;

LAB12:    xsi_set_current_line(125, ng0);
    t2 = (t0 + 1352U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t7 = ieee_p_2592010699_sub_1690584930_503743352(IEEE_P_2592010699, t5);
    t2 = (t0 + 4464);
    t6 = (t2 + 56U);
    t9 = *((char **)t6);
    t11 = (t9 + 56U);
    t15 = *((char **)t11);
    *((unsigned char *)t15) = t7;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(126, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t2 = (t0 + 2312U);
    t6 = *((char **)t2);
    t7 = *((unsigned char *)t6);
    t8 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t5, t7);
    t2 = (t0 + 1992U);
    t9 = *((char **)t2);
    t10 = *((unsigned char *)t9);
    t2 = (t0 + 2472U);
    t11 = *((char **)t2);
    t12 = *((unsigned char *)t11);
    t13 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t10, t12);
    t14 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t8, t13);
    t2 = (t0 + 2152U);
    t15 = *((char **)t2);
    t16 = *((unsigned char *)t15);
    t2 = (t0 + 2632U);
    t17 = *((char **)t2);
    t18 = *((unsigned char *)t17);
    t19 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t16, t18);
    t20 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t14, t19);
    t2 = (t0 + 4336);
    t21 = (t2 + 56U);
    t22 = *((char **)t21);
    t23 = (t22 + 56U);
    t24 = *((char **)t23);
    *((unsigned char *)t24) = t20;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(128, ng0);
    t3 = (25 * 1000LL);
    t2 = (t0 + 3760);
    xsi_process_wait(t2, t3);

LAB18:    *((char **)t1) = &&LAB19;
    goto LAB1;

LAB13:    goto LAB12;

LAB15:    goto LAB13;

LAB16:    xsi_set_current_line(129, ng0);
    t2 = (t0 + 1032U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t7 = ieee_p_2592010699_sub_1690584930_503743352(IEEE_P_2592010699, t5);
    t2 = (t0 + 4528);
    t6 = (t2 + 56U);
    t9 = *((char **)t6);
    t11 = (t9 + 56U);
    t15 = *((char **)t11);
    *((unsigned char *)t15) = t7;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(130, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t2 = (t0 + 2312U);
    t6 = *((char **)t2);
    t7 = *((unsigned char *)t6);
    t8 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t5, t7);
    t2 = (t0 + 1992U);
    t9 = *((char **)t2);
    t10 = *((unsigned char *)t9);
    t2 = (t0 + 2472U);
    t11 = *((char **)t2);
    t12 = *((unsigned char *)t11);
    t13 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t10, t12);
    t14 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t8, t13);
    t2 = (t0 + 2152U);
    t15 = *((char **)t2);
    t16 = *((unsigned char *)t15);
    t2 = (t0 + 2632U);
    t17 = *((char **)t2);
    t18 = *((unsigned char *)t17);
    t19 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t16, t18);
    t20 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t14, t19);
    t2 = (t0 + 4336);
    t21 = (t2 + 56U);
    t22 = *((char **)t21);
    t23 = (t22 + 56U);
    t24 = *((char **)t23);
    *((unsigned char *)t24) = t20;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(132, ng0);
    t3 = (25 * 1000LL);
    t2 = (t0 + 3760);
    xsi_process_wait(t2, t3);

LAB22:    *((char **)t1) = &&LAB23;
    goto LAB1;

LAB17:    goto LAB16;

LAB19:    goto LAB17;

LAB20:    xsi_set_current_line(133, ng0);
    t2 = (t0 + 1512U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t7 = ieee_p_2592010699_sub_1690584930_503743352(IEEE_P_2592010699, t5);
    t2 = (t0 + 4592);
    t6 = (t2 + 56U);
    t9 = *((char **)t6);
    t11 = (t9 + 56U);
    t15 = *((char **)t11);
    *((unsigned char *)t15) = t7;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(134, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t2 = (t0 + 2312U);
    t6 = *((char **)t2);
    t7 = *((unsigned char *)t6);
    t8 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t5, t7);
    t2 = (t0 + 1992U);
    t9 = *((char **)t2);
    t10 = *((unsigned char *)t9);
    t2 = (t0 + 2472U);
    t11 = *((char **)t2);
    t12 = *((unsigned char *)t11);
    t13 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t10, t12);
    t14 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t8, t13);
    t2 = (t0 + 2152U);
    t15 = *((char **)t2);
    t16 = *((unsigned char *)t15);
    t2 = (t0 + 2632U);
    t17 = *((char **)t2);
    t18 = *((unsigned char *)t17);
    t19 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t16, t18);
    t20 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t14, t19);
    t2 = (t0 + 4336);
    t21 = (t2 + 56U);
    t22 = *((char **)t21);
    t23 = (t22 + 56U);
    t24 = *((char **)t23);
    *((unsigned char *)t24) = t20;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(136, ng0);
    t3 = (25 * 1000LL);
    t2 = (t0 + 3760);
    xsi_process_wait(t2, t3);

LAB26:    *((char **)t1) = &&LAB27;
    goto LAB1;

LAB21:    goto LAB20;

LAB23:    goto LAB21;

LAB24:    xsi_set_current_line(137, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t7 = ieee_p_2592010699_sub_1690584930_503743352(IEEE_P_2592010699, t5);
    t2 = (t0 + 4656);
    t6 = (t2 + 56U);
    t9 = *((char **)t6);
    t11 = (t9 + 56U);
    t15 = *((char **)t11);
    *((unsigned char *)t15) = t7;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(138, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t5 = *((unsigned char *)t4);
    t2 = (t0 + 2312U);
    t6 = *((char **)t2);
    t7 = *((unsigned char *)t6);
    t8 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t5, t7);
    t2 = (t0 + 1992U);
    t9 = *((char **)t2);
    t10 = *((unsigned char *)t9);
    t2 = (t0 + 2472U);
    t11 = *((char **)t2);
    t12 = *((unsigned char *)t11);
    t13 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t10, t12);
    t14 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t8, t13);
    t2 = (t0 + 2152U);
    t15 = *((char **)t2);
    t16 = *((unsigned char *)t15);
    t2 = (t0 + 2632U);
    t17 = *((char **)t2);
    t18 = *((unsigned char *)t17);
    t19 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t16, t18);
    t20 = ieee_p_2592010699_sub_2545490612_503743352(IEEE_P_2592010699, t14, t19);
    t2 = (t0 + 4336);
    t21 = (t2 + 56U);
    t22 = *((char **)t21);
    t23 = (t22 + 56U);
    t24 = *((char **)t23);
    *((unsigned char *)t24) = t20;
    xsi_driver_first_trans_fast(t2);
    goto LAB2;

LAB25:    goto LAB24;

LAB27:    goto LAB25;

}


extern void work_a_2505427919_2372691052_init()
{
	static char *pe[] = {(void *)work_a_2505427919_2372691052_p_0};
	xsi_register_didat("work_a_2505427919_2372691052", "isim/Test_all_sum_isim_beh.exe.sim/work/a_2505427919_2372691052.didat");
	xsi_register_executes(pe);
}
