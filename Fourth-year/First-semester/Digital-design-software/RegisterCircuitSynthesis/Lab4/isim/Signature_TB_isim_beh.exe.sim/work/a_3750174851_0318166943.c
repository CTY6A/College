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
static const char *ng0 = "C:/Users/User/Desktop/LabsPOCP/Lab4/Signature.vhd";
extern char *IEEE_P_2592010699;

unsigned char ieee_p_2592010699_sub_1744673427_503743352(char *, char *, unsigned int , unsigned int );
unsigned char ieee_p_2592010699_sub_2507238156_503743352(char *, unsigned char , unsigned char );


static void work_a_3750174851_0318166943_p_0(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    char *t5;
    char *t6;
    char *t7;
    char *t8;
    char *t9;
    char *t10;

LAB0:    xsi_set_current_line(49, ng0);
    t1 = (t0 + 1192U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    t1 = (t0 + 992U);
    t3 = ieee_p_2592010699_sub_1744673427_503743352(IEEE_P_2592010699, t1, 0U, 0U);
    if (t3 != 0)
        goto LAB5;

LAB6:
LAB3:    t1 = (t0 + 4088);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(50, ng0);
    t1 = xsi_get_transient_memory(4U);
    memset(t1, 0, 4U);
    t5 = t1;
    memset(t5, (unsigned char)2, 4U);
    t6 = (t0 + 4200);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    t9 = (t8 + 56U);
    t10 = *((char **)t9);
    memcpy(t10, t1, 4U);
    xsi_driver_first_trans_fast(t6);
    goto LAB3;

LAB5:    xsi_set_current_line(52, ng0);
    t2 = (t0 + 1832U);
    t5 = *((char **)t2);
    t2 = (t0 + 4200);
    t6 = (t2 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t5, 4U);
    xsi_driver_first_trans_fast(t2);
    goto LAB3;

}

static void work_a_3750174851_0318166943_p_1(char *t0)
{
    char t23[16];
    char t24[16];
    char *t1;
    char *t2;
    int t3;
    unsigned int t4;
    unsigned int t5;
    unsigned int t6;
    unsigned char t7;
    char *t8;
    char *t9;
    int t10;
    int t11;
    int t12;
    unsigned int t13;
    unsigned int t14;
    unsigned int t15;
    unsigned char t16;
    unsigned char t17;
    char *t18;
    char *t19;
    char *t20;
    char *t21;
    char *t22;
    unsigned int t25;
    unsigned char t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;

LAB0:    xsi_set_current_line(58, ng0);
    t1 = (t0 + 1672U);
    t2 = *((char **)t1);
    t3 = (0 - 0);
    t4 = (t3 * 1);
    t5 = (1U * t4);
    t6 = (0 + t5);
    t1 = (t2 + t6);
    t7 = *((unsigned char *)t1);
    t8 = (t0 + 1672U);
    t9 = *((char **)t8);
    t10 = xsi_vhdl_pow(2, 2);
    t11 = (t10 - 1);
    t12 = (t11 - 0);
    t13 = (t12 * 1);
    t14 = (1U * t13);
    t15 = (0 + t14);
    t8 = (t9 + t15);
    t16 = *((unsigned char *)t8);
    t17 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t7, t16);
    t18 = (t0 + 4264);
    t19 = (t18 + 56U);
    t20 = *((char **)t19);
    t21 = (t20 + 56U);
    t22 = *((char **)t21);
    *((unsigned char *)t22) = t17;
    xsi_driver_first_trans_fast(t18);
    xsi_set_current_line(59, ng0);
    t1 = (t0 + 1672U);
    t2 = *((char **)t1);
    t3 = xsi_vhdl_pow(2, 2);
    t10 = (t3 - 1);
    t11 = (t10 - 0);
    t4 = (t11 * 1);
    t5 = (1U * t4);
    t6 = (0 + t5);
    t1 = (t2 + t6);
    t7 = *((unsigned char *)t1);
    t8 = (t0 + 1352U);
    t9 = *((char **)t8);
    t16 = *((unsigned char *)t9);
    t17 = ieee_p_2592010699_sub_2507238156_503743352(IEEE_P_2592010699, t7, t16);
    t8 = (t0 + 1672U);
    t18 = *((char **)t8);
    t13 = (0 - 0);
    t14 = (t13 * 1U);
    t15 = (0 + t14);
    t8 = (t18 + t15);
    t20 = ((IEEE_P_2592010699) + 4024);
    t21 = (t24 + 0U);
    t22 = (t21 + 0U);
    *((int *)t22) = 0;
    t22 = (t21 + 4U);
    *((int *)t22) = 2;
    t22 = (t21 + 8U);
    *((int *)t22) = 1;
    t12 = (2 - 0);
    t25 = (t12 * 1);
    t25 = (t25 + 1);
    t22 = (t21 + 12U);
    *((unsigned int *)t22) = t25;
    t19 = xsi_base_array_concat(t19, t23, t20, (char)99, t17, (char)97, t8, t24, (char)101);
    t25 = (1U + 3U);
    t26 = (4U != t25);
    if (t26 == 1)
        goto LAB2;

LAB3:    t22 = (t0 + 4328);
    t27 = (t22 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    memcpy(t30, t19, 4U);
    xsi_driver_first_trans_fast(t22);
    xsi_set_current_line(60, ng0);
    t1 = (t0 + 1992U);
    t2 = *((char **)t1);
    t7 = *((unsigned char *)t2);
    t1 = (t0 + 4328);
    t8 = (t1 + 56U);
    t9 = *((char **)t8);
    t18 = (t9 + 56U);
    t19 = *((char **)t18);
    *((unsigned char *)t19) = t7;
    xsi_driver_first_trans_delta(t1, 2U, 1, 0LL);
    t1 = (t0 + 4104);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_size_not_matching(4U, t25, 0);
    goto LAB3;

}

static void work_a_3750174851_0318166943_p_2(char *t0)
{
    char *t1;
    char *t2;
    char *t3;
    char *t4;
    char *t5;
    char *t6;
    char *t7;

LAB0:    xsi_set_current_line(63, ng0);

LAB3:    t1 = (t0 + 1672U);
    t2 = *((char **)t1);
    t1 = (t0 + 4392);
    t3 = (t1 + 56U);
    t4 = *((char **)t3);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    memcpy(t6, t2, 4U);
    xsi_driver_first_trans_fast_port(t1);

LAB2:    t7 = (t0 + 4120);
    *((int *)t7) = 1;

LAB1:    return;
LAB4:    goto LAB2;

}


extern void work_a_3750174851_0318166943_init()
{
	static char *pe[] = {(void *)work_a_3750174851_0318166943_p_0,(void *)work_a_3750174851_0318166943_p_1,(void *)work_a_3750174851_0318166943_p_2};
	xsi_register_didat("work_a_3750174851_0318166943", "isim/Signature_TB_isim_beh.exe.sim/work/a_3750174851_0318166943.didat");
	xsi_register_executes(pe);
}
