// Copyright Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/GeneratedCppIncludes.h"
#include "MythicIsles/Elevator.h"
PRAGMA_DISABLE_DEPRECATION_WARNINGS
void EmptyLinkFunctionForGeneratedCodeElevator() {}
// Cross Module References
	COREUOBJECT_API UScriptStruct* Z_Construct_UScriptStruct_FVector();
	ENGINE_API UClass* Z_Construct_UClass_AActor();
	MYTHICISLES_API UClass* Z_Construct_UClass_AElevator();
	MYTHICISLES_API UClass* Z_Construct_UClass_AElevator_NoRegister();
	UPackage* Z_Construct_UPackage__Script_MythicIsles();
// End Cross Module References
	void AElevator::StaticRegisterNativesAElevator()
	{
	}
	IMPLEMENT_CLASS_NO_AUTO_REGISTRATION(AElevator);
	UClass* Z_Construct_UClass_AElevator_NoRegister()
	{
		return AElevator::StaticClass();
	}
	struct Z_Construct_UClass_AElevator_Statics
	{
		static UObject* (*const DependentSingletons[])();
#if WITH_METADATA
		static const UECodeGen_Private::FMetaDataPairParam Class_MetaDataParams[];
#endif
#if WITH_METADATA
		static const UECodeGen_Private::FMetaDataPairParam NewProp_TargetPoint_MetaData[];
#endif
		static const UECodeGen_Private::FStructPropertyParams NewProp_TargetPoint;
#if WITH_METADATA
		static const UECodeGen_Private::FMetaDataPairParam NewProp_MyFloat_MetaData[];
#endif
		static const UECodeGen_Private::FFloatPropertyParams NewProp_MyFloat;
		static const UECodeGen_Private::FPropertyParamsBase* const PropPointers[];
		static const FCppClassTypeInfoStatic StaticCppClassTypeInfo;
		static const UECodeGen_Private::FClassParams ClassParams;
	};
	UObject* (*const Z_Construct_UClass_AElevator_Statics::DependentSingletons[])() = {
		(UObject* (*)())Z_Construct_UClass_AActor,
		(UObject* (*)())Z_Construct_UPackage__Script_MythicIsles,
	};
#if WITH_METADATA
	const UECodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AElevator_Statics::Class_MetaDataParams[] = {
		{ "IncludePath", "Elevator.h" },
		{ "ModuleRelativePath", "Elevator.h" },
	};
#endif
#if WITH_METADATA
	const UECodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AElevator_Statics::NewProp_TargetPoint_MetaData[] = {
		{ "Category", "Elevator" },
		{ "ModuleRelativePath", "Elevator.h" },
	};
#endif
	const UECodeGen_Private::FStructPropertyParams Z_Construct_UClass_AElevator_Statics::NewProp_TargetPoint = { "TargetPoint", nullptr, (EPropertyFlags)0x0010000000000001, UECodeGen_Private::EPropertyGenFlags::Struct, RF_Public|RF_Transient|RF_MarkAsNative, 1, nullptr, nullptr, STRUCT_OFFSET(AElevator, TargetPoint), Z_Construct_UScriptStruct_FVector, METADATA_PARAMS(Z_Construct_UClass_AElevator_Statics::NewProp_TargetPoint_MetaData, UE_ARRAY_COUNT(Z_Construct_UClass_AElevator_Statics::NewProp_TargetPoint_MetaData)) };
#if WITH_METADATA
	const UECodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AElevator_Statics::NewProp_MyFloat_MetaData[] = {
		{ "Category", "Elevator" },
		{ "ModuleRelativePath", "Elevator.h" },
	};
#endif
	const UECodeGen_Private::FFloatPropertyParams Z_Construct_UClass_AElevator_Statics::NewProp_MyFloat = { "MyFloat", nullptr, (EPropertyFlags)0x0010000000000001, UECodeGen_Private::EPropertyGenFlags::Float, RF_Public|RF_Transient|RF_MarkAsNative, 1, nullptr, nullptr, STRUCT_OFFSET(AElevator, MyFloat), METADATA_PARAMS(Z_Construct_UClass_AElevator_Statics::NewProp_MyFloat_MetaData, UE_ARRAY_COUNT(Z_Construct_UClass_AElevator_Statics::NewProp_MyFloat_MetaData)) };
	const UECodeGen_Private::FPropertyParamsBase* const Z_Construct_UClass_AElevator_Statics::PropPointers[] = {
		(const UECodeGen_Private::FPropertyParamsBase*)&Z_Construct_UClass_AElevator_Statics::NewProp_TargetPoint,
		(const UECodeGen_Private::FPropertyParamsBase*)&Z_Construct_UClass_AElevator_Statics::NewProp_MyFloat,
	};
	const FCppClassTypeInfoStatic Z_Construct_UClass_AElevator_Statics::StaticCppClassTypeInfo = {
		TCppClassTypeTraits<AElevator>::IsAbstract,
	};
	const UECodeGen_Private::FClassParams Z_Construct_UClass_AElevator_Statics::ClassParams = {
		&AElevator::StaticClass,
		"Engine",
		&StaticCppClassTypeInfo,
		DependentSingletons,
		nullptr,
		Z_Construct_UClass_AElevator_Statics::PropPointers,
		nullptr,
		UE_ARRAY_COUNT(DependentSingletons),
		0,
		UE_ARRAY_COUNT(Z_Construct_UClass_AElevator_Statics::PropPointers),
		0,
		0x009000A4u,
		METADATA_PARAMS(Z_Construct_UClass_AElevator_Statics::Class_MetaDataParams, UE_ARRAY_COUNT(Z_Construct_UClass_AElevator_Statics::Class_MetaDataParams))
	};
	UClass* Z_Construct_UClass_AElevator()
	{
		if (!Z_Registration_Info_UClass_AElevator.OuterSingleton)
		{
			UECodeGen_Private::ConstructUClass(Z_Registration_Info_UClass_AElevator.OuterSingleton, Z_Construct_UClass_AElevator_Statics::ClassParams);
		}
		return Z_Registration_Info_UClass_AElevator.OuterSingleton;
	}
	template<> MYTHICISLES_API UClass* StaticClass<AElevator>()
	{
		return AElevator::StaticClass();
	}
	DEFINE_VTABLE_PTR_HELPER_CTOR(AElevator);
	AElevator::~AElevator() {}
	struct Z_CompiledInDeferFile_FID_Users_ringe_Documents_GitHub_Mystic_Isles_MythicIsles_5_1_Source_MythicIsles_Elevator_h_Statics
	{
		static const FClassRegisterCompiledInInfo ClassInfo[];
	};
	const FClassRegisterCompiledInInfo Z_CompiledInDeferFile_FID_Users_ringe_Documents_GitHub_Mystic_Isles_MythicIsles_5_1_Source_MythicIsles_Elevator_h_Statics::ClassInfo[] = {
		{ Z_Construct_UClass_AElevator, AElevator::StaticClass, TEXT("AElevator"), &Z_Registration_Info_UClass_AElevator, CONSTRUCT_RELOAD_VERSION_INFO(FClassReloadVersionInfo, sizeof(AElevator), 2220708629U) },
	};
	static FRegisterCompiledInInfo Z_CompiledInDeferFile_FID_Users_ringe_Documents_GitHub_Mystic_Isles_MythicIsles_5_1_Source_MythicIsles_Elevator_h_1488021719(TEXT("/Script/MythicIsles"),
		Z_CompiledInDeferFile_FID_Users_ringe_Documents_GitHub_Mystic_Isles_MythicIsles_5_1_Source_MythicIsles_Elevator_h_Statics::ClassInfo, UE_ARRAY_COUNT(Z_CompiledInDeferFile_FID_Users_ringe_Documents_GitHub_Mystic_Isles_MythicIsles_5_1_Source_MythicIsles_Elevator_h_Statics::ClassInfo),
		nullptr, 0,
		nullptr, 0);
PRAGMA_ENABLE_DEPRECATION_WARNINGS
