import { useField } from 'formik';
import React from 'react';
import { Form, Label } from 'semantic-ui-react';
import DatePicker, {ReactDatePickerProps} from 'react-datepicker';



export default function MyDateInput(props:Partial<ReactDatePickerProps>) {
    const [field, meta, healpers] = useField(props.name! );
    return (
        <Form.Field error={meta.touched && !!meta.error }>
           <DatePicker
           {...field}
           {...props}
           selected={(field.value && new Date(field.value)) || null}
           onChange={value => healpers.setValue(value)}

           />
            {meta.touched && meta.error ? (
                <Label basic color='yellow'>{meta.error}</Label>
            ) : null}
        </Form.Field>
    )

}

