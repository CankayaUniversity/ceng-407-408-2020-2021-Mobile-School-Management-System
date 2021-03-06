﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilOkulProc.Entities.Concrete;

namespace MobilOkulProc.WebAPI.Data
{
    public class clsSchooll_Proccess:EfEntityRepository<SCHOOL, MobilOkulContext> {}
    public class clsKurumlar:EfEntityRepository<SCHOOL, MobilOkulContext>{}
    public class clsUser_Proccess:EfEntityRepository<USER, MobilOkulContext> {}
    public class clsUserAuth_Proccess : EfEntityRepository<AuthenticationResponse, MobilOkulContext> { }
    public class clsStudent_Process: EfEntityRepository<STUDENT,MobilOkulContext>{}
    public class clsBranch_Process : EfEntityRepository<BRANCH, MobilOkulContext>{}
    public class clsEducationalTerm_Process : EfEntityRepository<EDUCATIONAL_TERM, MobilOkulContext>{}
    public class clsEmployeeType_Process : EfEntityRepository<EMPLOYEE_TYPE, MobilOkulContext>{}
    public class clsFeedback_Process : EfEntityRepository<FEEDBACK, MobilOkulContext>{}
    public class clsMessages_Process : EfEntityRepository<MESSAGE, MobilOkulContext>{}
    public class clsNews_Process : EfEntityRepository<NEWS, MobilOkulContext>{}
    public class clsTeacher_Process : EfEntityRepository<TEACHER, MobilOkulContext>{}
    public class clsStudentParent_Process : EfEntityRepository<STUDENT_PARENT, MobilOkulContext>{}
    public class clsStudentClass_Process : EfEntityRepository<STUDENT_CLASS, MobilOkulContext>{}
    public class clsSection_Process : EfEntityRepository<SECTION, MobilOkulContext>{}
    public class clsSchoolEmployer_Process : EfEntityRepository<SCHOOL_EMPLOYER, MobilOkulContext>{}
    public class clsParent_Process : EfEntityRepository<PARENT, MobilOkulContext>{}
    public class clsCity_Process : EfEntityRepository<CITY, MobilOkulContext>{}
    public class clsClassSection_Process : EfEntityRepository<CLASS_SECTION, MobilOkulContext>{}
    public class clsEducationalInstitution_Process : EfEntityRepository<EDUCATIONAL_INSTITUTION, MobilOkulContext>{}
    public class clsClass_Process : EfEntityRepository<CLASS, MobilOkulContext>{}
    public class clsTeacherSchool_Process: EfEntityRepository<TEACHER_SCHOOL, MobilOkulContext> { }
    public class clsSchoolStudent_Process: EfEntityRepository<SCHOOL_STUDENT,MobilOkulContext> { }
    public class clsLecture_Process: EfEntityRepository<LECTURE, MobilOkulContext> { }
    public class clsGrade_Process : EfEntityRepository<GRADE, MobilOkulContext> { }
    public class clsSyllabus_Process : EfEntityRepository<SYLLABUS, MobilOkulContext> { }
    public class clsDays_Process : EfEntityRepository<DAYS, MobilOkulContext> { }
    public class clsExam_Process : EfEntityRepository<EXAM, MobilOkulContext> { }
    public class clsGradeType_Process : EfEntityRepository<GRADETYPE, MobilOkulContext> { }
    public class clsAbsence_Process : EfEntityRepository<ABSENCE, MobilOkulContext> { }

}
