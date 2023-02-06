set serveroutput on;

declare 
    tableId fsi_rt_match_service.N_MATCH_TABLE_ID%TYPE;
    seqId fsi_rt_match_service.N_MATCH_TABLE_ID%TYPE;
    minusId VARCHAR2(50);
begin
    select max(N_MATCH_TABLE_ID) into tableId from fsi_rt_match_service;
    select SEQ_MATCH_SERVICE.NEXTVAL into seqId from dual;
    minusId := to_char(tableid-seqid-1);
    DBMS_OUTPUT.PUT_LINE(tableId);
    DBMS_OUTPUT.PUT_LINE(seqid);
    DBMS_OUTPUT.PUT_LINE(minusId);
    execute immediate 'alter sequence SEQ_MATCH_SERVICE increment by ' || minusId;
    select SEQ_MATCH_SERVICE.NEXTVAL into seqId from dual;
    execute immediate 'alter sequence SEQ_MATCH_SERVICE increment by 1';
    select SEQ_MATCH_SERVICE.NEXTVAL into seqId from dual;
    DBMS_OUTPUT.PUT_LINE(seqid);
end;