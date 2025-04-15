import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import {
  ActivityIndicator,
  BackHandler,
  NativeSyntheticEvent,
  Pressable,
  Text,
  TextInput,
  TextInputKeyPressEventData,
  TextInputProps,
  View
} from "react-native";
import { CustomInput } from "../login/LoginForm";
import Input from "../../Input";
import Button from "../../Button";
import {
  IDENTIFIER_TYPE,
  ModifyIdentifierParams,
  useRequestUpdateIdentifier,
  useUpdateIdentifier,
  useVerifyUpdateIdentifierOTP
} from "@/api/user";
import {
  saveAuthData,
  selectModifyIdentifierData,
  selectResetModifyIdentifierForm
} from "@/slices/auth.slice";
import { colors } from "@/constants/colors";
import { Controller, UseFormReturn, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useBounce } from "@/hooks";
import Animated, {
  SlideInLeft,
  SlideInRight,
  SlideOutLeft,
  SlideOutRight
} from "react-native-reanimated";
import { useAppDispatch } from "@/store/hooks";
import BackButton from "@/components/BackButton";
import { useRouter } from "expo-router";
import { useErrorHandler } from "@/hooks/useErrorHandler";
import useSyncUser from "@/hooks/user/useSyncUser";
import { useTranslation } from "react-i18next";
import useVerifyIdentifierSchema from "@/hooks/form/auth/useVerifyIdentifierSchema";

export type VerifyUpdateIdentifierFormProps = {
  className?: string;
};

export type VerifyFormFields = Array<string>;

export type StepProps<T extends {}> = Pick<
  UseFormReturn<T>,
  "control" | "trigger" | "watch"
> & {
  errors: Pick<UseFormReturn<T>, "formState">["formState"]["errors"];
  goToNextStep: () => void;
  goToPrevStep: () => void;
  type: IDENTIFIER_TYPE;
};

export type Step<T> = {
  fields: Array<keyof T>;
};

export type UpdateIdentifierFormFields = {
  OTP?: string | undefined;
  identifier: string;
};

export const VerifyUpdateIdentifierForm = ({
  className
}: VerifyUpdateIdentifierFormProps) => {
  const { type } = selectModifyIdentifierData() as ModifyIdentifierParams;
  const { schema } = useVerifyIdentifierSchema(type);

  const {
    trigger,
    control,
    formState: { errors },
    watch,
    reset
  } = useForm({
    resolver: yupResolver(schema)
  });

  const [currentStep, setCurrentStep] = useState(1);
  const [prevStep, setPrevStep] = useState<number>(1);
  /** The gap between the current and previous step. */
  const deltaStep = useMemo(() => currentStep - prevStep, [currentStep]);

  const steps: Step<UpdateIdentifierFormFields>[] = useMemo(
    () => [{ fields: ["identifier"] }, { fields: ["OTP"] }, { fields: [] }],
    []
  );

  const Steps = useMemo(() => [UpdateIdentifer, VerifyOtp, Success], []);

  const goToNextStep = useCallback(() => {
    if (currentStep === steps.length) {
      return;
    }

    setPrevStep(currentStep);
    setCurrentStep(prev => prev + 1);
  }, [currentStep, steps.length]);

  const goToPrevStep = useCallback(() => {
    if (currentStep === 1) {
      return;
    }

    const key = steps[currentStep - 1]?.fields?.[0];

    if (key) {
      reset({ [key]: undefined });
    }

    setPrevStep(currentStep);
    setCurrentStep(prev => prev - 1);
  }, [currentStep]);

  const router = useRouter();

  const hanldeBackButtonPressed = useCallback(() => {
    currentStep === 1 ? router.back() : goToPrevStep();
  }, [currentStep]);

  useEffect(() => {
    const backHanlder = BackHandler.addEventListener("hardwareBackPress", () => {
      hanldeBackButtonPressed();
      return true;
    });

    return () => backHanlder.remove();
  }, []);

  const isResetNeeded = selectResetModifyIdentifierForm();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (isResetNeeded) {
      dispatch(saveAuthData({ resetModifyIdentifierForm: false }));
      reset();

      if (currentStep !== 1) {
        setCurrentStep(1);
        setPrevStep(1);
      }
    }
  }, [isResetNeeded]);

  return (
    <View className={`gap-20 ${className}`}>
      <BackButton
        onPress={hanldeBackButtonPressed}
        className='bg-white_black200 w-[38px] rounded-xl border border-black px-4 py-3.5 dark:border-white'
      />

      {Steps.map((Step, index) =>
        currentStep === index + 1 ? (
          <Animated.View
            key={currentStep}
            entering={deltaStep > 0 ? SlideInRight : SlideInLeft}
            exiting={deltaStep > 0 ? SlideOutRight : SlideOutLeft}
          >
            <Step
              type={type}
              control={control}
              trigger={trigger}
              errors={errors}
              goToNextStep={goToNextStep}
              goToPrevStep={goToPrevStep}
              watch={watch}
            />
          </Animated.View>
        ) : null
      )}
    </View>
  );
};

export const VerifyOtp = ({
  type,
  control,
  trigger,
  errors,
  goToNextStep,
  watch
}: StepProps<UpdateIdentifierFormFields>) => {
  const [isLoading, setIsLoading] = useState(false);
  const [countdown, setCountdown] = useState(0);

  const { mutateAsync: resendOtp } = useRequestUpdateIdentifier();
  const { mutateAsync: verifyOtp } = useVerifyUpdateIdentifierOTP();
  const { mutateAsync: updateIdentifier } = useUpdateIdentifier();

  const OTP = watch("OTP");
  const identifier = watch("identifier");
  const isButtonDisabled = useMemo(() => !OTP, [OTP]);

  const { animate, animatedStyles } = useBounce();
  const { handleError } = useErrorHandler();
  const { t } = useTranslation("verifyUpdateIdentifier", { keyPrefix: "otp" });

  useEffect(() => {
    const interval = setInterval(() => {
      setCountdown(prevCount => prevCount - 1);
    }, 1000);

    return () => clearInterval(interval);
  }, [countdown]);

  const handleResendOtp = () => {
    if (countdown > 0) return;

    resendOtp(
      { type, data: { identifier } },
      {
        onSuccess: () => setCountdown(30),
        onError: error => handleError(error)
      }
    );
  };

  const submit = async () => {
    await trigger("OTP", { shouldFocus: true });
    if (errors.OTP || !OTP) return;

    setIsLoading(true);

    const params = { type, data: { identifier, OTP } };

    await verifyOtp(params, {
      onSuccess: async () => {
        await updateIdentifier(params, {
          onSuccess: () => goToNextStep(),
          onError: error => handleError(error),
          onSettled: () => setIsLoading(false)
        });
      },
      onError: error => {
        handleError(error);
        setIsLoading(false);
      }
    });
  };

  return (
    <View className='gap-10'>
      <View>
        <Text className='text-black_white font-sans font-semibold text-4xl'>
          {t("title")}
        </Text>
        <Text className='font-sans text-lg text-gray-300'>
          {`${t("description")} `} <Text className='text-primary'>{identifier}</Text>
        </Text>
      </View>

      <View className='gap-3.5'>
        <Controller
          name='OTP'
          control={control}
          render={({ field: { onChange, onBlur } }) => {
            return (
              <OtpInput
                onChangeText={val => {
                  console.log("value", val);
                  onChange(val);
                }}
                onBlur={onBlur}
              />
            );
          }}
        />
        {errors.OTP ? (
          <Text className='font-sans text-red-400'>{errors.OTP.message}</Text>
        ) : null}
      </View>

      <Button
        onPress={() => {
          animate();
          submit();
        }}
        style={[animatedStyles]}
        className='h-20 justify-center rounded-full bg-primary p-3 text-white'
        isLoading={isLoading}
        disabled={isButtonDisabled}
        spinner={
          <ActivityIndicator
            className='text-white'
            animating={isLoading}
          />
        }
      >
        <Text className='text-center text-xl text-white'>{t("confirm")}</Text>
      </Button>

      <Pressable
        onPress={handleResendOtp}
        disabled={countdown > 0}
      >
        <Text className='text-black_white text-center text-lg'>
          {`${t("dontReceiveCode")} `}
          {countdown > 0 ? (
            <Text className='text-gray-500'>{`${t("pleaseWaitFor")} ${countdown}s`}</Text>
          ) : (
            <Text className='text-primary'>{t("promptResend")}</Text>
          )}
        </Text>
      </Pressable>
    </View>
  );
};

const UpdateIdentifer = ({
  control,
  trigger,
  errors,
  goToNextStep,
  type,
  watch
}: StepProps<UpdateIdentifierFormFields>) => {
  const [isLoading, setIsLoading] = useState(false);

  const identifier = watch("identifier");
  const isButtonDisabled = useMemo(() => !identifier, [identifier]);

  const { mutateAsync: requestUpdate } = useRequestUpdateIdentifier();

  const { animate, animatedStyles } = useBounce();
  const { handleError } = useErrorHandler();
  const { t } = useTranslation("verifyUpdateIdentifier", { keyPrefix: "update" });

  const submit = async () => {
    await trigger("identifier", { shouldFocus: true });
    if (errors.identifier) return;

    setIsLoading(true);

    await requestUpdate(
      {
        type,
        data: {
          identifier: identifier as string
        }
      },
      {
        onSuccess: async () => goToNextStep(),
        onError: error => handleError(error),
        onSettled: () => setIsLoading(false)
      }
    );
  };

  return (
    <View className='gap-14'>
      <View>
        <Text className='text-black_white font-sans font-semibold text-4xl'>
          {t("title")}
        </Text>
        <Text className='font-sans text-lg text-gray-300'>
          {`${t("description")} `}
          {type === IDENTIFIER_TYPE.EMAIL ? t("email") : t("phone")}
        </Text>
      </View>
      <View className='gap-3'>
        <Controller
          name='identifier'
          control={control}
          render={({ field: { onChange, value, onBlur } }) => (
            <CustomInput
              inputMode={type === IDENTIFIER_TYPE.EMAIL ? "email" : "tel"}
              value={value}
              onBlur={onBlur}
              onChangeText={value => {
                onChange(value);
                if (errors.identifier) trigger("identifier");
              }}
            />
          )}
        />
        {errors.identifier ? (
          <Text className='font-sans text-red-400'>{errors.identifier.message}</Text>
        ) : null}
      </View>

      <Button
        onPress={() => {
          animate();
          submit();
        }}
        style={[animatedStyles]}
        className='h-20 justify-center rounded-full bg-primary p-3 text-white'
        isLoading={isLoading}
        spinner={
          <ActivityIndicator
            className='text-white'
            animating={isLoading}
          />
        }
        disabled={isButtonDisabled}
      >
        <Text className='text-center text-xl text-white'>{t("update")}</Text>
      </Button>
    </View>
  );
};

const Success = (_: Partial<StepProps<UpdateIdentifierFormFields>>) => {
  const [count, setCount] = useState(5);

  const { fetch: fetchUser } = useSyncUser();

  const router = useRouter();
  const dispatch = useAppDispatch();
  const { t } = useTranslation("verifyUpdateIdentifier", { keyPrefix: "success" });

  useEffect(() => {
    fetchUser();
  }, []);

  useEffect(() => {
    if (count === 0) {
      router.replace("/");
      dispatch(saveAuthData({ resetModifyIdentifierForm: true }));
    }

    const interval = setInterval(() => {
      setCount(prevCount => prevCount - 1);
    }, 1000);

    return () => clearInterval(interval);
  }, [count]);

  return (
    <View className='gap-14'>
      <View>
        <Text className='text-black_white font-sans font-semibold text-4xl'>
          {t("title")}
        </Text>
        <Text className='font-sans text-lg text-green-700 dark:text-green-400'>
          {t("description")}
        </Text>
      </View>
      <Text className='text-black_white font-sans text-lg'>
        {`${t("redirect")} `} <Text className='text-primary'>{count}s</Text>
      </Text>
    </View>
  );
};

export const OtpInput = ({
  onChangeText,
  className,
  ...props
}: Pick<
  TextInputProps,
  | "onChangeText"
  | "onBlur"
  | "placeholder"
  | "defaultValue"
  | "className"
  | "secureTextEntry"
>) => {
  const inputRefs = Array.from({ length: 6 }, () => useRef<TextInput>(null));
  const { primary } = colors;
  const [isHandlingKeyPress, setIsHandlingKeyPress] = useState(false);
  const [isHandlingKeyPressSuccessfully, setIsHandlingKeyPressSuccessfully] =
    useState(false);
  const [formValues, setFormValues] = useState<VerifyFormFields>([
    "",
    "",
    "",
    "",
    "",
    ""
  ]);

  useEffect(() => {
    onChangeText?.(formValues.join(""));
  }, [formValues]);

  const handleTextChange = (value: string, index: number) => {
    if (value.length === 2 && index < inputRefs.length - 1) {
      handleTextChange(value.slice(1), index + 1);
      return;
    }

    const firstChar = value.charAt(0).toUpperCase();

    setFormValues(
      formValues.map((currentChar, idx) => (idx === index ? firstChar : currentChar))
    );

    if (value.length > 0 && index < inputRefs.length - 1) {
      inputRefs[index + 1].current?.focus();
    } else if (
      value.length === 0 &&
      index > 0 &&
      !isHandlingKeyPress &&
      !isHandlingKeyPressSuccessfully
    ) {
      inputRefs[index - 1].current?.focus();
    }
  };

  const handleKeyPress = (
    e: NativeSyntheticEvent<TextInputKeyPressEventData>,
    index: number
  ) => {
    setIsHandlingKeyPress(true);
    setIsHandlingKeyPressSuccessfully(false);

    try {
      if (index === 0) return;
      if (e.nativeEvent.key === "Backspace") {
        inputRefs[index - 1].current?.focus();
      }

      setIsHandlingKeyPressSuccessfully(true);
    } finally {
      setIsHandlingKeyPress(false);
    }
  };

  return (
    <View className={`flex flex-row gap-2 ${className}`}>
      {inputRefs.map((ref, index) => (
        <Input
          value={formValues[index]}
          onChangeText={value => handleTextChange(value, index)}
          onKeyPress={e => handleKeyPress(e, index)}
          inputMode='numeric'
          key={index}
          ref={ref}
          autoCapitalize='characters'
          className={`aspect-square w-[53px] shrink grow border-gray-300 text-center text-primary focus:border-primary`}
          autoFocus={index === 0}
          cursorColor={primary}
          {...props}
        />
      ))}
    </View>
  );
};

export default VerifyUpdateIdentifierForm;
