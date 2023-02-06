SELECT * FROM dim_sanc_swift_msg_details where n_sanc_swift_msg_id  = 310001; -- first
select * from dim_sanctions_swift_details where n_sanction_swift_msg_type = 310001; --second
select * from FSI_RT_SWIFT_CONF_DTLS where N_SWIFT_MSG_ID = 53336; -- third
select * from FSI_RT_MSG_TAGS_CONF where n_swift_msg_id = 53336;
select * from FSI_RT_MATCH_SERVICE where N_SWIFT_MSG_ID = 53336 order by n_webservice_id,v_field_name;
select * from FSI_RT_swift_expr where N_SWIFT_MSG_ID = 53336;


select * from dim_sanctions_swift_details where n_sanction_swift_msg_type = 72; --second
delete from FSI_RT_SWIFT_CONF_DTLS where N_SWIFT_MSG_ID = 53496; -- third
delete from FSI_RT_MSG_TAGS_CONF where n_swift_msg_id = 53496;
delete from FSI_RT_MATCH_SERVICE where N_SWIFT_MSG_ID = 53496 order by n_webservice_id,v_field_name;
delete from FSI_RT_swift_expr where N_SWIFT_MSG_ID = 53496;


select * from FSI_RT_MATCH_SERVICE_details;
select * from dim_swift_webservice;
--select * from FSI_RT_SWIFT_CONF_DTLS_HIST;


create table dim_sanc_swift_msg_details_31 as 
SELECT * FROM dim_sanc_swift_msg_details;
create table dim_sanctions_swift_details_31 as 
SELECT * FROM dim_sanctions_swift_details;
create table FSI_RT_SWIFT_CONF_DTLS_31 as 
SELECT * FROM FSI_RT_SWIFT_CONF_DTLS;
create table FSI_RT_MSG_TAGS_CONF_31 as 
SELECT * FROM FSI_RT_MSG_TAGS_CONF;
create table FSI_RT_MATCH_SERVICE_31 as 
SELECT * FROM FSI_RT_MATCH_SERVICE;
create table FSI_RT_swift_expr_31 as 
SELECT * FROM FSI_RT_swift_expr;




SELECT * FROM dim_sanc_swift_msg_details where n_sanc_swift_msg_id  = 310002; -- first
delete from dim_sanctions_swift_details where n_sanction_swift_msg_type = 310002; --second
delete from FSI_RT_SWIFT_CONF_DTLS where N_SWIFT_MSG_ID = 52084; -- third
delete from FSI_RT_MSG_TAGS_CONF where n_swift_msg_id = 52084;
delete from FSI_RT_MATCH_SERVICE where N_SWIFT_MSG_ID = 52084 order by n_webservice_id,v_field_name;
delete from FSI_RT_swift_expr where N_SWIFT_MSG_ID = 52084;


drop table dim_sanc_swift_msg_details_31;
drop table dim_sanctions_swift_details_31;
drop table FSI_RT_SWIFT_CONF_DTLS_31;
drop table FSI_RT_MSG_TAGS_CONF_31;
drop table FSI_RT_MATCH_SERVICE_31;
drop table FSI_RT_swift_expr_31;






select * from fsi_rt_match_service order by N_MATCH_TABLE_ID desc
;
select SEQ_MATCH_SERVICE.NEXTVAL from dual;
alter sequence SEQ_MATCH_SERVICE increment by 18;

alter sequence SEQ_MATCH_SERVICE restart start with 20071;