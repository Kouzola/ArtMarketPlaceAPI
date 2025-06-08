import { ControlContainer, ValidatorFn, ValidationErrors, AbstractControl } from "@angular/forms";

export function FileImageValidators(): ValidatorFn{
    return (control: AbstractControl): ValidationErrors | null => {
        const file = control.value;
        const allowedTypes = ['image/png', 'image/jpeg'];
        if(!file){
            return null;
        }
        //max file size 1MB
    if (file instanceof File || (file?.size && file?.type)) {
      const errors: ValidationErrors = {};

        if (file.size > 1024 * 1024) {
        errors['maxFileSize'] = true;
      }

      if (!allowedTypes.includes(file.type)) {
        errors['invalidFileType'] = true;
      }

      return Object.keys(errors).length > 0 ? errors : null;
    }
        return null;
    }
}